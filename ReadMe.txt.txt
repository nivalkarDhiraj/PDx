******************************************************************************
*	Created : Milind Gulhane                                                 *
*	Contact : milind.gulhane@atos.net                                        *
******************************************************************************
# HCA Lab Test
	This IS Web API application for lab test data handing and reporting

# Problem statement
	Need application that is capable of
	1. Generate authentication token for security access
	2. Creating/Managing Patient securely
	3. Creating/Managing Test securely
	4. Creating/Managing/Reporting Test Reports securely

# Tables (As model classes for In-Memory DB implementation)
	LoggedInUser
		string Username //Logged in user name
		string Password //User password
		
	Patient
        int Id //PrimarKey
		string PatientName //Patient Name
		DateTime DateOfBirth //Date of birth of patient
        Enum PatientGender //Gender of patient (0 - None, 1 - Male, 2 - Female, 3 - Other)
		string EmailId //Patient email id
		string ContactNumber //Patient contact number
		string Address //Patient address
		bool isDeleted //To mark for soft delete
	
	LabTest
		int Id //PrimarKey
		Enum TestType //Type of test (0 - Chemical, 1 - Physical)
		string Description //Description of the test
		Enum SampleType //Type of sample for the test (0 - None, 1 - Blood, 2 - Urine, 3 - Stool, 4 - Swab, 5 - Other)
		Single MinimumRequiredQty //Minimum amount of quantity of sample needed for test
		double MinLimit //Minimum limit of value for the result
		double MaxLimit //Maximum limit of value for the result
		bool isDeleted //To mark for soft delete
		
	LabReport
		int Id //PrimarKey
		int PatientId //ForeignKey Patient.Id
		LabTestId  //ForeignKey LabTest.Id
		DateTime SampleReceivedOn //Sample received on
		DateTime SampleTestedOn //Sample tested on
		DateTime ReportCreatedOn //Report cretated on
		double TestResult //Value of test result
		string RefferredBy //Refferred by physician / hospital
		bool NeedConsultation //Recommeded for consultation
		bool isDeleted //To mark for soft delete
		
# Approach
	Implemention using In-Memory DB
	User credentials in LoggedInUser class
	Patient details in Patient class
	Test details in LabTest class
	Report details in LabReport class
	Delete is soft delete (Marked isDeleted true)
	Restore possible for soft deleted records (Marked isDeleted false)
	
#Operations Supported
	Controllerwise operations supported as below with sample URL and payload information 
	
	1. Login
		* Login : (Post : https://localhost:44367/Login)
			{
				"username": "Demouser",
				"password": "DemoPassword"
			}			
	2. Patient
		* Create : (Post : https://localhost:44367/Patient/Create)
			{
				"id": 0,
				"patientName": "Test Patient 1",
				"dateOfBirth": "1980-05-25T00:00:00",
				"patientGender": 1,
				"emailId": "testpatient1@gmail.com",
				"contactNumber": "(+91) 98235xxxxx",
				"address": "Pune, Maharashtra, India - 411018"
			}
		* Update : (Put : https://localhost:44367/Patient/Update/1)
			{
				"id": 1,
				"patientName": "Test Patient 1 Modified",
				"dateOfBirth": "1980-05-25T00:00:00",
				"patientGender": 1,
				"emailId": "testpatient1@gmail.com",
				"contactNumber": "(+91) 98235xxxxx",
				"address": "Pune, Maharashtra, India - 411018"
			}
		* Delete : (Delete : https://localhost:44367/Patient/Delete/1)
		* Restore : (Put : https://localhost:44367/Patient/Restore/1)
		* GetAll : (Get : https://localhost:44367/Patient/Get)
		* GetById : (Get : https://localhost:44367/Patient/Get/1)
		
	3. LabTest
		* Create : (Post : https://localhost:44367/LabTest/Create)
			{
				"id": 0,
				"testType": 1,
				"description": "Blood Count",
				"sampleType": 1,
				"minimumRequiredQty": 50,
				"minLimit": 100,
				"maxLimit": 1000
			}		
		* Update : (Put : https://localhost:44367/LabTest/Update/1)
			{
				"id": 1,
				"testType": 1,
				"description": "Blood Count Modified",
				"sampleType": 1,
				"minimumRequiredQty": 500,
				"minLimit": 500,
				"maxLimit": 5000
			}		
		* Delete : (Delete : https://localhost:44367/LabTest/Delete/1)
		* Restore : (Put : https://localhost:44367/LabTest/Restore/1)
		* GetAll : (Get : https://localhost:44367/LabTest/Get)
		* GetById : (Get : https://localhost:44367/LabTest/Get/1)
		
	4. LabReport
		* Create : (Post : https://localhost:44367/LabReport/Create)
			{
				"id": 0,
				"patientId": 1,
				"labTestId": 1,
				"sampleReceivedOn": "2021-01-10T00:00:00",
				"sampleTestedOn": "2021-01-11T00:00:00",
				"reportCreatedOn": "2021-01-12T00:00:00",
				"testResult": 125,
				"refferredBy": "Dr. Physician 1"
			}
		* Update : (Put : https://localhost:44367/LabReport/Update/1)
			{
				"id": 1,
				"patientId": 1,
				"labTestId": 1,
				"sampleReceivedOn": "2021-01-10T00:00:00",
				"sampleTestedOn": "2021-01-11T00:00:00",
				"reportCreatedOn": "2021-01-12T00:00:00",
				"testResult": 125,
				"refferredBy": "Dr. Physician 1 Modified"
			}
		* Delete : (Delete : https://localhost:44367/LabReport/Delete/1)
		* Restore : (Put : https://localhost:44367/LabReport/Restore/1)
		* GetAll : (Get : https://localhost:44367/LabReport/Get)
		* GetById : (Get : https://localhost:44367/LabReport/Get/1)
		* GetByLabTest : (Get : https://localhost:44367/LabReport/GetByLabTest/1/2021-01-01/2021-12-31)
		
#Installation
	1. Copy code in a folder
	2. Open LabTest soultion using Microsoft Visual Studio
	3. Build and Run project HCA.API.LabTests
	4. Application should run in browser using Swagger UI
	5. Postman can also be configured (as per above url and payload details) for generating and passing token
	
#Steps to run
	1. Login (Credentials as above) to generate token
	2. Once token is generated, copy the generated token to pass with subsequet requests
	2. Create Patient (if executed Get(), will create 4 hardcoded Patients from backed when Patient table is empty)
	3. Create LabTest (if executed Get(), will create 4 hardcoded Tests from backed when LabTest table is empty)
	4. Create LabReport (if executed Get(), will create 4 hardcoded LabReports from backed when LabReport table is empty)
