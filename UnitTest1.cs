using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace AddressBookApiProject
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void SetUp()
        {
            //Initialize the base URL to execute requests made by the instance
            client = new RestClient("http://localhost:3000");
        }
        private IRestResponse GetContactList()
        {
            //Arrange
            //Initialize the request object with proper method and URL
            RestRequest request = new RestRequest("/contacts/list", Method.GET);
            //Act
            // Execute the request
            IRestResponse response = client.Execute(request);
            return response;
        }
        /// <summary>
        /// UC 22 : Reads the entries from json server.
        /// </summary>
        [TestMethod]
        public void ReadEntriesFromJsonServer()
        {
            IRestResponse response = GetContactList();
            // Check if the status code of response equals the default code for the method requested
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            // Convert the response object to list of employees
            List<contact> employeeList = JsonConvert.DeserializeObject<List<contact>>(response.Content);
            Assert.AreEqual(4, employeeList.Count);
            foreach (contact c in employeeList)
            {
                Console.WriteLine($"Id: {c.id}\tFullName: {c.firstName} {c.lastName}\tPhoneNo: {c.phoneNumber}\tAddress: {c.address}\tCity: {c.city}\tState: {c.state}\tZip: {c.zip}\tEmail: {c.email}");
            }
        }
    }
}
