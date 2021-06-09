﻿using System.Collections.Generic;

namespace Device
{
    /// <summary>
    /// Технологическое устройство - световая сигнализация.
    /// </summary>
    public class HL : IODevice
    {
        public HL(string name, string eplanName, string description,
            int deviceNumber, string objectName, int objectNumber,
            string articleName) : base(name, eplanName, description,
                deviceNumber, objectName, objectNumber)
        {
            dSubType = DeviceSubType.NONE;
            dType = DeviceType.HL;
            ArticleName = articleName;

            DO.Add(new IOChannel("DO", -1, -1, -1, ""));
        }

        public override string Check()
        {
            string res = base.Check();

            if (ArticleName == "")
            {
                res += $"\"{name}\" - не задано изделие.\n";
            }

            return res;
        }

        public override string GetDeviceSubTypeStr(DeviceType dt,
            DeviceSubType dst)
        {
            switch (dt)
            {
                case DeviceType.HL:
                    return dt.ToString();
            }
            return "";
        }

        public override Dictionary<string, int> GetDeviceProperties(
            DeviceType dt, DeviceSubType dst)
        {
            switch (dt)
            {
                case DeviceType.HL:
                    return new Dictionary<string, int>()
                    {
                        {"ST", 1},
                        {"M", 1},
                    };
            }
            return null;
        }
    }
}
