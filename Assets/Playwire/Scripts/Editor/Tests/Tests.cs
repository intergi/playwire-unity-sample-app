using System;
using System.Globalization;
using System.Threading;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class EventMessageTests
    {
        [Test]
        public void FromEmptyStringShouldReturnNullMessage()
        {
            PlaywireSDKEventMessage message = PlaywireSDKEventMessage.CreateFromJSON("");

            Assert.IsNull(message);
        }

        [Test]
        public void FromJsonWithNoValuesShouldContainNullProperties()
        {
            string json = GetJson(null, null, null, false);

            PlaywireSDKEventMessage message = PlaywireSDKEventMessage.CreateFromJSON(json);

            Assert.IsNotNull(message);
            Assert.IsNull(message.name);
            Assert.IsNull(message.adUnitId);
            Assert.IsNull(message.parameters);
        }

        [Test]
        public void FromJsonWithNullValuesShouldContainEmptyProperties()
        {
            string json = GetJson(null, null, null, true);

            PlaywireSDKEventMessage message = PlaywireSDKEventMessage.CreateFromJSON(json);

            Assert.IsNotNull(message);
            Assert.IsEmpty(message.name);
            Assert.IsEmpty(message.adUnitId);
            Assert.IsEmpty(message.parameters);
        }

        [Test]
        public void FromJsonWithNameValueAndNullsShouldReturnValidMessage()
        {
            string expectedEventName = "On_TestEvent";
            
            string json = GetJson(expectedEventName, null, null, true);
            PlaywireSDKEventMessage message = PlaywireSDKEventMessage.CreateFromJSON(json);

            Assert.IsNotNull(message);
            Assert.IsNotEmpty(message.name);
            Assert.AreEqual(message.name, expectedEventName);
            Assert.IsEmpty(message.adUnitId);
            Assert.IsEmpty(message.parameters);
        }

        [Test]
        public void FromJsonWithOnlyNameValueShouldReturnValidMessage()
        {
            string expectedEventName = "On_TestEvent";
            
            string json = GetJson(expectedEventName, null, null, false);
            PlaywireSDKEventMessage message = PlaywireSDKEventMessage.CreateFromJSON(json);

            Assert.IsNotNull(message);
            Assert.IsNotEmpty(message.name);
            Assert.AreEqual(message.name, expectedEventName);
            Assert.IsNull(message.adUnitId);
            Assert.IsNull(message.parameters);
        }

        [Test]
        public void FromJsonWithOnlyNameAndUnitIdValuesShouldReturnValidMessage()
        {
            string expectedEventName = "On_TestEvent";
            string expectedAdUnitId = "TestAdUnitId";

            string json = GetJson(expectedEventName, expectedAdUnitId, null, true);
            PlaywireSDKEventMessage message = PlaywireSDKEventMessage.CreateFromJSON(json);

            Assert.IsNotNull(message);
            Assert.IsNotEmpty(message.name);
            Assert.AreEqual(message.name, expectedEventName);
            Assert.IsNotEmpty(message.adUnitId);
            Assert.AreEqual(message.adUnitId, expectedAdUnitId);
            Assert.IsEmpty(message.parameters);
        }

        [Test]
        public void FromJsonWithValuesShouldReturnValidMessageWithSameValues()
        {
            string parameters = "{\"value\":\"Test Key\"}";
            byte[] parametersData = System.Text.Encoding.UTF8.GetBytes(parameters);
            string expectedParameters = System.Convert.ToBase64String(parametersData);
            string expectedEventName = "On_TestEvent";
            string expectedAdUnitId = "TestAdUnitId";

            string json = GetJson(expectedEventName, expectedAdUnitId, expectedParameters, false);
            PlaywireSDKEventMessage message = PlaywireSDKEventMessage.CreateFromJSON(json);

            Assert.IsNotNull(message);
            Assert.IsNotEmpty(message.name);
            Assert.AreEqual(message.name, expectedEventName);
            Assert.IsNotEmpty(message.adUnitId);
            Assert.AreEqual(message.adUnitId, expectedAdUnitId);
            Assert.IsNotEmpty(message.parameters);
            Assert.AreEqual(message.parameters, expectedParameters);
        }

        #region JSONHelpers

        private static string GetJsonEntry(string key, object value, bool addNullValues)
        {
            return value != null ? "\"" + key + "\":\"" + value + "\"," : (addNullValues ? "\"" + key + "\":" + "null" + "," : "");
        }

        private static string GetJson(string name, string adUnitId, string parameters, bool addNullValues)
        {
            string entries = GetJsonEntry("name", name, addNullValues) 
                            + GetJsonEntry("adUnitId", adUnitId, addNullValues) 
                            + GetJsonEntry("parameters", parameters, addNullValues);
            return "{" + entries.Trim(',') + "}";
        }

        #endregion JSONHelpers
    }
}