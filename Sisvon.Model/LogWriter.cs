using System;
using System.IO;

namespace Sisvon.Model
{
    public class LogWriter : IDisposable
    {
        //Ctor
        public LogWriter(string logMessage, string path)
        {
            LogWrite(logMessage, path);
        }
        public LogWriter(Exception logException, string path)
        {
            LogWrite(logException, path);
        }
        //Dispose
        public void Dispose()
        {
        }
        //StringLog
        private void LogWrite(string logMessage, string path)
        {
            try
            {
                if (!CriarPasta(path)) return;
                using (StreamWriter w = File.AppendText(path + "\\" + "log.txt"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
        //ExceptionLog
        private void LogWrite(Exception exception, string path)
        {
            try
            {
                if (!CriarPasta(path)) return;
                using (StreamWriter w = File.AppendText(path + "\\" + "log.txt"))
                {
                    Log(exception, w);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void Log(Exception exception, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  : Exception");
                txtWriter.WriteLine("  :{0}", exception.Message);
                txtWriter.WriteLine("  : Source");
                txtWriter.WriteLine("  :{0}", exception.Source);
                txtWriter.WriteLine("  : StackTrace");
                txtWriter.WriteLine("  :{0}", exception.StackTrace);
                txtWriter.WriteLine("--------------------------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
        private bool CriarPasta(string path)
        {
            bool result = true;
            if (Directory.Exists(path)) return result;
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception exception)
            {
                result = false;
            }
            return result;
        }

    }
}
