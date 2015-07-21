using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AetherQuest
{
    /* Written By: Jesse Mitchell
     * 
     * The logger logs all of the data passed to it.
     * 
     */
    class Logger
    {
        private static Logger instance = new Logger();
        private StreamWriter writer;
        public Logger()
        {
            if (instance == null)
            {
                String dir = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/AetherQuest/";
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);


                writer = new StreamWriter(dir + "log.txt");
            }
            else throw new Exception("Do not instansiate the Logger class. Use getInstance()");
        }

        /// <summary>
        /// Returns the Instance of the Logger
        /// </summary>
        /// <returns></returns>
        public static Logger getInstance()
        {
            return instance;
        }

        /// <summary>
        /// Logs the data passed to it.
        /// </summary>
        /// <param name="text">Data to be written.</param>
        public void log(String text)
        {
            writer.WriteLine(text);
            writer.Flush();
        }
        
    }


}
