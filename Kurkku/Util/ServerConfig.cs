using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Kurkku.Util
{
    class ServerConfig
    {
        #region Fields

        private static readonly string m_ConfigFileName = "config.xml";
        private static readonly ServerConfig m_ServerConfig = new ServerConfig();
        private Dictionary<string, string> m_ConfigValues;

        #endregion

        #region Properties

        /// <summary>
        /// Get the singleton instance
        /// </summary>
        public static ServerConfig Instance
        {
            get
            {
                return m_ServerConfig;
            }
        }

        public string ConnectionString
        {
            get
            {
                var connectionString = new MySqlConnectionStringBuilder();
                connectionString.Server = ServerConfig.Instance.GetString("mysql", "hostname");
                connectionString.Port = (uint)ServerConfig.Instance.GetInt("mysql", "port");
                connectionString.UserID = ServerConfig.Instance.GetString("mysql", "username");
                connectionString.Password = ServerConfig.Instance.GetString("mysql", "password");
                connectionString.Database = ServerConfig.Instance.GetString("mysql", "database");
                connectionString.MinimumPoolSize = (uint)ServerConfig.Instance.GetInt("mysql", "mincon");
                connectionString.MaximumPoolSize = (uint)ServerConfig.Instance.GetInt("mysql", "maxcon");
                return connectionString.ToString();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Attempt to read configuration file
        /// </summary>
        public ServerConfig()
        {
            if (m_ConfigValues == null)
                m_ConfigValues = new Dictionary<string, string>();

            if (!File.Exists(m_ConfigFileName))
            {
                WriteConfig();
            }

            m_ConfigValues.Clear();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(m_ConfigFileName);

            SetConfig(xmlDoc, "mysql/hostname");
            SetConfig(xmlDoc, "mysql/username");
            SetConfig(xmlDoc, "mysql/password");
            SetConfig(xmlDoc, "mysql/database");
            SetConfig(xmlDoc, "mysql/port");
            SetConfig(xmlDoc, "mysql/min_connections", "mincon");
            SetConfig(xmlDoc, "mysql/max_connections", "maxcon");
            SetConfig(xmlDoc, "server/ip");
            SetConfig(xmlDoc, "server/port");
        }

        private void SetConfig(XmlDocument xmlDoc, string xmlPath, string configKey = null)
        {
            try
            {
                m_ConfigValues[configKey != null ? xmlPath.Split('/')[0] + "/" + configKey : xmlPath] = xmlDoc.SelectSingleNode("//configuration/" + xmlPath).InnerText;
            }
            catch
            {
                m_ConfigValues[configKey != null ? xmlPath.Split('/')[0] + "/" + configKey : xmlPath] = string.Empty;
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Attempts to write configuration file
        /// </summary>
        private void WriteConfig()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("   ");
            settings.OmitXmlDeclaration = true;

            XmlWriter xmlWriter = XmlWriter.Create(m_ConfigFileName, settings);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("configuration");
            xmlWriter.WriteStartElement("mysql");

            xmlWriter.WriteStartElement("hostname");
            xmlWriter.WriteString("localhost");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("username");
            xmlWriter.WriteString("root");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("password");
            xmlWriter.WriteString("");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("database");
            xmlWriter.WriteString("Kurkku");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("port");
            xmlWriter.WriteString("3306");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("min_connections");
            xmlWriter.WriteString("5");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("max_connections");
            xmlWriter.WriteString("10");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("server");

            xmlWriter.WriteStartElement("ip");
            xmlWriter.WriteString("127.0.0.1");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("port");
            xmlWriter.WriteString("91");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get string by key
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>the string</returns>
        public string GetString(string category, string item)
        {
            return m_ConfigValues.GetValueOrDefault(string.Concat(category, "/", item));
        }

        /// <summary>
        /// Get integer by key
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>the integer</returns>
        public int GetInt(string category, string item)
        {
            int number = 0;
            int.TryParse(m_ConfigValues.GetValueOrDefault(string.Concat(category, "/", item)), out number);
            return number;
        }

        #endregion
    }
}
