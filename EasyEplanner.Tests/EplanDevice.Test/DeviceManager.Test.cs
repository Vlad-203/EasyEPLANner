﻿using NUnit.Framework;

namespace Tests.EplanDevices
{
    public class DeviceManagerTest
    {
        [SetUp]
        public void SetUpDevices()
        {
            var firstValve = new EplanDevice.V("LINE1V2", "+LINE1-V2", "Test valve",
                2, "LINE", 1, "Test V article");
            firstValve.SetSubType("V_AS_MIXPROOF");
            firstValve.SetParameter("R_AS_NUMBER", 1);
            EplanDevice.DeviceManager.GetInstance().Devices.Add(firstValve);

            var secondValve = new EplanDevice.V("TANK2V1", "+LINE2-V2", "Test valve",
                1, "TANK", 2, "Test V article");
            secondValve.SetSubType("V_AS_MIXPROOF");
            secondValve.SetParameter("R_AS_NUMBER", 2);
            EplanDevice.DeviceManager.GetInstance().Devices.Add(secondValve);

            var pressureSensor = new EplanDevice.PT("KOAG3PT1", "+KOAG3-PT1", 
                "Test PT", 1, "KOAG", 3, "Test PT article");
            pressureSensor.SetSubType("PT_IOLINK");
            EplanDevice.DeviceManager.GetInstance().Devices.Add(pressureSensor);

            var temperatureSensor = new EplanDevice.TE("BATH4TE2", "+BATH4-TE2",
                "Test TE", 2, "BATH", 4, "Test TE article");
            temperatureSensor.SetSubType("TE");
            EplanDevice.DeviceManager.GetInstance().Devices.Add(temperatureSensor);
        }

        [TestCase("+LINE1-V2", false)]
        [TestCase("+LINE1-V2 +TANK2-V1", true)]
        [TestCase("+TANK2-V1 +KOAG3-PT1", null)]
        [TestCase("LINE1V2", false)]
        [TestCase("+LINE1-V2 TANK2V1", false)]
        [TestCase("TANK2V1 KOAG3PT1", false)]
        public void IsASInterfaseDeviceTest(string devices, bool? expected)
        {
            bool? actual = EplanDevice.DeviceManager.GetInstance()
                .IsASInterfaceDevices(devices, out _);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("+LINE-1V2", true)]
        [TestCase("LINE1V2", true)]
        [TestCase("+TANK99-TE99", true)]
        [TestCase("KOAG99PT99", true)]
        [TestCase("MKK1", false)]
        [TestCase("DI2", true)]
        [TestCase("FC2", true)]
        [TestCase("TC1", true)]
        [TestCase("TRC2", true)]
        [TestCase("TKK1", false)]
        public void CheckDeviceNameTest(string device, bool expected)
        {
            bool actual = EplanDevice.DeviceManager.GetInstance()
                .CheckDeviceName(device, out _, out _, out _, out _, out _,
                out _);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("+LINE1-V2", 0)]
        [TestCase("+TANK2-V1", 1)]
        [TestCase("+KOAG3-PT1", 2)]
        [TestCase("+BATH4-TE2", 3)]
        [TestCase("LINE1V2", 0)]
        [TestCase("TANK2V1", 1)]
        [TestCase("KOAG3PT1", 2)]
        [TestCase("BATH4TE2", 3)]
        [TestCase("BATH4TE99", -1)]
        [TestCase("", -1)]
        public void GetDeviceIndex(string device, int expectedNum)
        {
            int actualNum = EplanDevice.DeviceManager.GetInstance()
                .GetDeviceIndex(device);
            Assert.AreEqual(expectedNum, actualNum);
        }

        [TestCase("+LINE1-V2", "LINE1V2")]
        [TestCase("+TANK2-V1", "TANK2V1")]
        [TestCase("+KOAG3-PT1", "KOAG3PT1")]
        [TestCase("+BATH4-TE2", "BATH4TE2")]
        [TestCase("LINE1V2", "LINE1V2")]
        [TestCase("TANK2V1", "TANK2V1")]
        [TestCase("KOAG3PT1", "KOAG3PT1")]
        [TestCase("BATH4TE2", "BATH4TE2")]
        [TestCase("BATH4TE99", StaticHelper.CommonConst.Cap)]
        [TestCase("", "")]
        public void GetDeviceTest(string devName, string expectedDevName)
        {
            var dev = EplanDevice.DeviceManager.GetInstance().GetDevice(devName);
            string actualDevName = dev.Name;
            if (expectedDevName == StaticHelper.CommonConst.Cap)
            {
                Assert.AreEqual(expectedDevName, dev.Description);
            }
            else
            {
                Assert.AreEqual(expectedDevName, actualDevName);
            }
        }

        [TestCase("+LINE1-V2", "LINE1V2")]
        [TestCase("+TANK2-V1", "TANK2V1")]
        [TestCase("+KOAG3-PT1", "KOAG3PT1")]
        [TestCase("+BATH4-TE2", "BATH4TE2")]
        [TestCase("LINE1V2", "LINE1V2")]
        [TestCase("TANK2V1", "TANK2V1")]
        [TestCase("KOAG3PT1", "KOAG3PT1")]
        [TestCase("BATH4TE2", "BATH4TE2")]
        [TestCase("BATH4TE99", StaticHelper.CommonConst.Cap)]
        [TestCase("", "")]
        public void GetDeviceByEplanNameTest(string devName, string expectedDevName)
        {
            var dev = EplanDevice.DeviceManager.GetInstance()
                .GetDeviceByEplanName(devName);
            string actualDevName = dev.Name;
            if (expectedDevName == StaticHelper.CommonConst.Cap)
            {
                Assert.AreEqual(expectedDevName, dev.Description);
            }
            else
            {
                Assert.AreEqual(expectedDevName, actualDevName);
            }
        }
    }
}
