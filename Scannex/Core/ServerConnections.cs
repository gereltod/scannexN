using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;
using System.Net.Http;

namespace Scannex
{
    public static class ServerConnections
    {
        public static string ACCESS_TOKEN = "";

        static T CreateT<T>(bool _new) where T : new()
        {
            if (_new) return new T(); else return default(T);
        }

        private static String _CorrectResponseString = String.Empty;

        public static String Login(string username, string pwd)
        {
            string ret = string.Empty;
            HttpWebResponse response = null;
            HttpWebRequest req = null;
            try
            {
                req = (HttpWebRequest)WebRequest.Create(new Uri(Constants.SERVER_URL + "oauth/token"));
                req.Headers.Set("Cache-Control", "no-cache");
                req.Method = "POST";
                req.KeepAlive = true;
                req.ContentType = "application/x-www-form-urlencoded";

                string post = "client_id=1&client_secret=7tDmIFmIDsyr1osuRlhsb3vf43cfhWAgdIsd2T2o&username=" + username + "&password=" + pwd + "&scope=*&grant+type=password";

                req.ContentLength = post.Length;
                req.SendChunked = true;
                StreamWriter swOut = new StreamWriter(req.GetRequestStream());
                swOut.Write(post);
                swOut.Flush();
                swOut.Close();

                response = (HttpWebResponse)req.GetResponse();

                WebHeaderCollection header = response.Headers;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var encoding = ASCIIEncoding.UTF8;
                    using (var reader = new StreamReader(response.GetResponseStream(), encoding))
                    {
                        string responseText = reader.ReadToEnd();
                        dynamic aa = JsonConvert.DeserializeObject(responseText);
                        ACCESS_TOKEN = aa.access_token;
                        ret = "200";
                        Constants.USERNAME = username;
                        Constants.ISLOGIN = true;
                    }
                }
                else if(response.StatusCode == HttpStatusCode.Forbidden)
                {
                    ret = "403";
                    Constants.ISLOGIN = false;
                }

            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                   ret= ((HttpWebResponse)e.Response).StatusCode.ToString();
                }
                
            }            
            catch (Exception ex)
            {
                FileLogger.LogStringInFile(ex.Message);
                Constants.ISLOGIN = false;
                ret = "error";
            }

            return ret;
        }

        public static T ServerGETData<T>(string url)
        {
            T res;
            try
            {
                
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(Constants.SERVER_URL + url));
                req.Headers.Set("Cache-Control", "no-cache");
                req.Method = "GET";
                req.KeepAlive = true;
                req.Headers.Add("Authorization", "Bearer " + ACCESS_TOKEN);
                
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();

                WebHeaderCollection header = response.Headers;

                var encoding = ASCIIEncoding.UTF8;
                using (var reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    string responseText = reader.ReadToEnd();
                    res = JsonConvert.DeserializeObject<T>(responseText);
                }

                return res;
            }
            catch (Exception ex)
            {
                FileLogger.LogStringInFile(ex.Message);
                return default(T);
            }
        }

        public static String Server(byte[] img)
        {
            String res;
            try
            {

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(Constants.SERVER_URL + "api/s3-signature"));
                req.Headers.Set("Cache-Control", "no-cache");
                req.Method = "GET";
                req.KeepAlive = true;
                req.Headers.Add("Authorization", "Bearer " + ACCESS_TOKEN);

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();

                WebHeaderCollection header = response.Headers;

                var encoding = ASCIIEncoding.UTF8;
                using (var reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    string responseText = reader.ReadToEnd();
                    string[] b = responseText.Split(':');
                    
                    res = SendServer(img, b[8].Split(',')[0].Replace('"', ' ').Trim(), b[4].Split(',')[0].Replace('"', ' ').Trim(), @"AKIAIZGID2M7YCKFURUA/20171007/us-east-1/s3/aws4_request", b[7].Split(',')[0].Replace('"', ' ').Trim());
                }

                return res;
            }
            catch (Exception ex)
            {
                FileLogger.LogStringInFile(ex.Message);
                return "";
            }
        }

        public static string SendServer(byte[] arrImage, string sign,string policy, string xcred, string xdate)
        {
            string ret = "";
            try
            {
                var multipart = new MultipartFormDataContent();
                multipart.Add(new StringContent(""), "Content-Type");
                multipart.Add(new StringContent("private"), "acl");
                multipart.Add(new StringContent("201"), "success_action_status");
                multipart.Add(new StringContent(policy), "policy");
                multipart.Add(new StringContent(xcred), "X-amz-credential");
                multipart.Add(new StringContent("AWS4-HMAC-SHA256"), "X-amz-algorithm");
                multipart.Add(new StringContent(xdate), "X-amz-date");
                multipart.Add(new StringContent(sign), "X-amz-signature");

                multipart.Add(new StringContent("3x6gjmyv34q8kvwl/7blindsyooo.pdf"), "key");
                multipart.Add(new ByteArrayContent(arrImage), "file");
                var httpClient = new HttpClient();
                var response = httpClient.PostAsync(new Uri("https://s3.amazonaws.com/staging.smartdrawers.com"), multipart).Result;

                string d = response.Content.ReadAsStringAsync().Result;
                ret = d;
            }
            catch
            {
                ret = null;
            }

            return ret;
        }

        public static T ServerData<T>(string url, string contentType)
        {
            T res;
            try
            {
                //T res = Activator.CreateInstance<T>();

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(Constants.SERVER_URL + url));
                req.Headers.Set("Cache-Control", "no-cache");
                req.Method = "POST";
                req.KeepAlive = true;
                req.ContentType = contentType;

                // Original method to send HTTP request
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();

                WebHeaderCollection header = response.Headers;

                var encoding = ASCIIEncoding.UTF8;
                using (var reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    string responseText = reader.ReadToEnd();
                    res = JsonConvert.DeserializeObject<T>(responseText);
                }

                return res;
            }
            catch(Exception ex)  {
                FileLogger.LogStringInFile(ex.Message);
                return default(T); }
        }

        public static bool Delete(string url, string id)
        {
            bool ret = false;

            try
            {
                string ur = String.Format("{0}{1}/{2}", Constants.SERVER_URL, url, id);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(ur));
                req.Headers.Set("Cache-Control", "no-cache");
                req.Method = "DELETE";
                //req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();

                WebHeaderCollection header = response.Headers;

                var encoding = ASCIIEncoding.UTF8;
                using (var reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    string responseText = reader.ReadToEnd();
                    if (responseText.Contains("success"))
                        ret = true;
                }
            }
            catch { }
            return ret;
        }
 

        //public static string InsertImage(byte[] arrImage, string filename, string fileext)
        //{
        //    string ret = "";
        //    try
        //    {
        //        var multipart = new MultipartFormDataContent();
        //        multipart.Add(new ByteArrayContent(arrImage), "uploads", filename);
        //        multipart.Add(new StringContent(fileext), "fileext");
        //        var httpClient = new HttpClient();
        //        var response = httpClient.PostAsync(new Uri(Core.Constants.URL + "upload"), multipart).Result;

        //        string d = response.Content.ReadAsStringAsync().Result;
        //        ret = d;
        //    }
        //    catch
        //    {
        //        ret = null;
        //    }
        //    return ret;
        //}

        public static Bitmap ReadImage(string name)
        {
            System.Drawing.Bitmap bmp;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(Constants.SERVER_URL + "uploads/" + name));
                req.Method = "GET";
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();
                bmp = new System.Drawing.Bitmap(myResponse.GetResponseStream());
                myResponse.Close();
            }
            catch { bmp = null; }
            return bmp;
        }
    }
}

