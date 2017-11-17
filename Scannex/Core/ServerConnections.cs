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
                        Constants.EXPIRE_TIME = aa.expires_in;
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
                else
                {
                    Constants.ISLOGIN = false;
                    ret = "error";
                }
                FileLogger.LogStringInFile(e.Message);
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

        public static string ServerPostData(string url, string post)
        {
            string ret = "";
            HttpWebResponse response = null;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(Constants.SERVER_URL + url));
                req.Headers.Set("Cache-Control", "no-cache");
                req.Method = "POST";
                req.KeepAlive = true;
                req.Headers.Add("Authorization", "Bearer " + ACCESS_TOKEN);
                
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
                    }
                }

                ret = "OK";
            }
            catch (Exception ex)
            {
                FileLogger.LogStringInFile(ex.Message);
                ret = ex.Message;
            }

            return ret;
        }

        public static String ServerFile(string filename, byte[] img)
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
                    var sign = JsonConvert.DeserializeObject<Signature>(responseText);

                    res = SendServerAWS(filename, img, sign);
                }

                return res;
            }
            catch (Exception ex)
            {
                FileLogger.LogStringInFile(ex.Message);
                return "";
            }
        }
                
        public static string SendServerAWS(string fileName, byte[] arrImage, Signature signature)
        {
            string ret = "";
            try
            {
                var multipart = new MultipartFormDataContent();
                multipart.Add(new StringContent(""), "Content-Type");
                multipart.Add(new StringContent(signature.Acl), "acl");
                multipart.Add(new StringContent("201"), "success_action_status");
                multipart.Add(new StringContent(signature.Policy), "policy");
                multipart.Add(new StringContent(signature.XamzCredential), "X-amz-credential");
                multipart.Add(new StringContent(signature.XamzAlgorithm), "X-amz-algorithm");
                multipart.Add(new StringContent(signature.XamzDate), "X-amz-date");
                multipart.Add(new StringContent(signature.XamzSignature), "X-amz-signature");

                string f = signature.Key.Replace("${filename}", fileName);
                multipart.Add(new StringContent(f), "key");
                multipart.Add(new ByteArrayContent(arrImage), "file");
                var httpClient = new HttpClient();
                var response = httpClient.PostAsync(new Uri("https://s3.amazonaws.com/staging.smartdrawers.com"), multipart).Result;

                string d = response.Content.ReadAsStringAsync().Result;
                ret = d;
            }
            catch(Exception ex)
            {
                FileLogger.LogStringInFile(ex.Message);
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

