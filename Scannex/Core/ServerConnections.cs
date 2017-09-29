using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;

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

                string post = "client_id=1&client_secret=LJzFy35Tyc5L4IJdlJIQQXPBnkMSVb3ZPqTakMJY&username=" + username + "&password=" + pwd + "&scope=*&grant+type=password";

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

        public static bool InsertArtist(string url, string post, bool isUpdate, out string id)
        {
            bool ret = false;
            id = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(Constants.SERVER_URL + url));
                if (!isUpdate)
                    req.Method = "POST";
                else
                    req.Method = "PUT";

                req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                req.ContentLength = post.Length;
                req.SendChunked = true;
                StreamWriter swOut = new StreamWriter(req.GetRequestStream());
                swOut.Write(post);
                swOut.Flush();
                swOut.Close();

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();

                WebHeaderCollection header = response.Headers;

                var encoding = ASCIIEncoding.UTF8;
                using (var reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    string responseText = reader.ReadToEnd();
                    if (responseText.Contains("success"))
                    {
                        string[] a = responseText.Split('-');
                        if (a.Length > 1)
                            id = a[1];

                        ret = true;
                    }
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

