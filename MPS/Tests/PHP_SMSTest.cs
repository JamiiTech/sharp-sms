#region Includes
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using MPS;
#endregion

///////////////////////////////////////////////////////////////////////////////
// Copyright 2009 (c) by Microsoft All Rights Reserved.
//  
// Project:      
// Module:       PHP_SMSTest.cs
// Description:  Tests for the PHP SMS class in the MPS assembly.
//  
// Date:       Author:           Comments:
// 15.03.2009 22:47  Admin     Module created.
///////////////////////////////////////////////////////////////////////////////
namespace MPSTest
{

    /// <summary>
    /// Tests for the PHP SMS Class
    /// Documentation: Обработка SMS путём вызова обработчиков на PHP
    /// </summary>
    [TestFixture(Description="Tests for PHP SMS")]
    public class PHP_SMSTest
    {
        #region Class Variables
        private PHP_SMS _phpSms = null;
        #endregion

        #region Setup/Teardown

        /// <summary>
        /// Code that is run once for a suite of tests
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {

        }

        /// <summary>
        /// Code that is run once after a suite of tests has finished executing
        /// </summary>
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {

        }

        /// <summary>
        /// Code that is run before each test
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            //New instance of PHP SMS
            _phpSms = new PHP_SMS("test");
        }

        /// <summary>
        /// Code that is run after each test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            //TODO:  Put dispose in here for _phpSms or delete this line
        }
        #endregion

        #region Property Tests

//No public properties were found. No tests are generated for non-public scoped properties.

        #endregion

        #region Method Tests


        /// <summary>
        /// do Post Method Test
        /// Documentation   :  Выполнение POST запроса
        /// Method Signature:  string doPost() 
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void doPostTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            string expected = "test";
            string results;

            //Parameters
            
            results = _phpSms.doPost();
            Assert.AreEqual(expected, results, "MPS.PHP_SMS.doPost method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("MPS.PHP_SMS.doPost Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Get Prefix Method Test
        /// Documentation   :  
        /// Method Signature:  string Get_Prefix(string smsText) 
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void Get_PrefixTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            string expected = "test";
            string results;

            //Parameters
            string smsText = "test";

            results = PHP_SMS.Get_Prefix(smsText);
            Assert.AreEqual(expected, results, "MPS.PHP_SMS.Get_Prefix method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("MPS.PHP_SMS.Get_Prefix Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// PHP SMS Constructor Test
        /// Documentation   :  Получить URL обработчика из БД по префиксу
        /// Constructor Signature:  PHP_SMS Get_by_Prefix(string prefix) 
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void PHP_SMSConstructorTest()
        {
            System.DateTime constructorStartTime = System.DateTime.Now;
                        
            //Parameters
            string prefix = "test";

             PHP_SMS phpSms = new PHP_SMS(prefix);
            Assert.AreEqual(String.Empty, String.Empty, "MPS.PHP_SMS.PHP_SMSConstructor constructor test failed");

            System.TimeSpan constructorDuration = System.DateTime.Now.Subtract(constructorStartTime);
            System.Console.WriteLine(String.Format("MPS.PHP_SMS.PHP_SMSConstructor Time Elapsed: {0}", constructorDuration.ToString()));
        }

        /// <summary>
        /// Upload Values Method Test
        /// Documentation   :  
        /// Method Signature:  string UploadValues() 
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void UploadValuesTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            string expected = "test";
            string results;

            //Parameters
            
            results = _phpSms.UploadValues();
            Assert.AreEqual(expected, results, "MPS.PHP_SMS.UploadValues method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("MPS.PHP_SMS.UploadValues Time Elapsed: {0}", methodDuration.ToString()));
        }


        #endregion

    }
}
