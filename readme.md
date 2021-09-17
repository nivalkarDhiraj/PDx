# API Development

The API has been developed using .Net core 3.1 framework.

The service has three endpoints. The auth endpoint can be used to generate token, patient for patient CRUD operation and lab report for lab report CRUD operation.

## Overview

To use this API, please clone the code from repository for this branch, build and run on local.

The JWT basic authentication has been implemented to and needs to generate token with valid credential and pass this token as bearer token, to use any API endpoints. The API auth endpoint can be used to generate token by passing credential details as Basic-Auth and using authorization header. The /auth/token method can also be refer to generate token by passing credential.

The in-memory cache has been implemented to support all CURD operation, and all data will be loosed once the API is stopped. You can use distributed cache as well if required or not using stick-session. The helpter class for that has been added as well and due to simplicty to test the design and solution InMemory cache has been used.

## Solution file

The solution file (.sln) is inside folder **HCA.PlatformDigital**) which can be used to open and explore different projects.

## API Sample Payload

The sample payload for each controller method is added in folder **SamplePayload** as of part of API project, which can be refer to test API.


## API Documentation


The swagger has been integrated for API documentation purpose. The swagger endpoint is the default endpoint ({baseurl}/swagger) on local when executed. The swagger main page can be used to refer the sample payload for different endpoints. The swagger Authorized button at right side corner can be use to provide token at single place as (bearer {token}) to test endpoints.

Please refer below controller and steps to test the API: 

#### Note: Any HttpTestClient can be used to test the API, during developement postman and swagger has been used to test API.

### API Controller (Auth)


baseurl/auth: Please use this endpoint to generate token by providing valid credential as basic-auth as HttpHeader parameters. Please refer appsettings.json file with section identityconfiguration and key (email, accesskey) as credential.

baseurl/auth/token: This method is used to generate token by providing the identity credential in body as Json.

### Patient Controller


**/patient/create:** This endpoint can be used to create patient with below payload.

```json
{
  "patientId": "",
  "name": "James John",
  "dob": "1972-10-15",
  "isMale": true,
  "contactNo": ""
}
```
Patient name and DOB are required field and patient id will be autogenerated. 

**/patient/update:** This endpoint can be used to update patient details and below sample payload can be for this.
```json
{
  "patientId": "",
  "name": "James John",
  "dob": "1972-10-15",
  "isMale": true,
  "contactNo": ""
}
```
The patient id, name and DOB are required field. The patient already created can be updated only based on auto-generated patient id. 


**/patient/delete:** This endpoint can be used to delete patient already created. Please refer below endpoint and parameter to delete patient.

/delete/{patientId}

A valid patient id needs to be passed as route parameter to delete patient. You can verify patient is deleted or not can refer patient list endpoint. 

**/patient/get:** This endpoint can be used to get list of patients. 


**/patient/get/{patientid}:** This endpoint can be used to get specific patient details by patient id. 

**patient/filter/{testname}/{startDate}/{endDate}:** This endpoint can be used to filter patients list who have conducted specific test and with test date range. 

The test name is mandatory parameter for this endpoint and date range (sate date and end date) is optional.

### LabReport Controller


**/labreport/create:** This endpoint can be used to create lab report for a patient and patient should exists in the system. The below payload can be used to create lab report.
```json
{
  "reportId": "",
  "reportName": "Blood Test",
  "reportTime": "2021-01-01T07:16:59.706Z",
  "patientId": "6d349d02-53b1-407f-bedb-2668b4d988b4", -- actual patient id
  "labTests": [
    {
      "testName": "CBC",
      "testDateTime": "2021-09-14T07:16:59.706Z",
      "tests": [
        {
          "name": "WHITE BLOOD CELL COUNT",
          "result": "3.9",
          "resultExpected": "3.8-10.8 Thousand/uL",
          "technology": "PHOTOMETRY",
          "method": "GOD-PAP METHOD",
          "description": "CBC ",
          "testParameters": [
            {
              "name": "",
              "value": ""
            }
          ]
        },
        {
          "name": "RED BLOOD CELL COUNT",
          "result": "5.24",
          "resultExpected": "4.20 - 5.80 Million/uL",
          "technology": "PH",
          "method": "GOD-PAP METHOD",
          "description": "CBC TEST",
          "testParameters": [
            {
              "name": "",
              "value": ""
            }
          ]
        },
        {
          "name": "HEMOGLOBIN",
          "result": "16.5",
          "resultExpected": "13.2 - 17.1 g/dL",
          "technology": "PH",
          "method": "GOD-PAP METHOD",
          "description": "CBC ",
          "testParameters": [
            {
              "name": "",
              "value": ""
            }
          ]
        },
        {
          "name": "MCV",
          "result": "94.9",
          "resultExpected": "80.0 - 100.0 fL",
          "technology": "PH",
          "method": "GOD-PAP METHOD",
          "description": "CBC ",
          "testParameters": [
            {
              "name": "Param1",
              "value": "This is to describe more test property"
            }
          ]
        }
      ],
      "description": "desc"
    }
  ]
}
```

Patient id is required field for this endpoint and patient should exists for which you want to create lab report.

**/labreport/update:** This endpoint can be used to update 
lab report for a patient. The lab report and patient must exist to update lab report, so please pass valid report id and patient id to update lab report. The below payload can be referred to update lab report.

```json
{
  "reportId": "6d349d02-53b1-407f-bedb-2668b4d988b4",
  "reportName": "Blood Test",
  "reportTime": "2021-01-01T07:16:59.706Z",
  "patientId": "6d349d02-53b1-407f-bedb-2668b4d988b4", -- actual patient id
  "labTests": [
    {
      "testName": "CBC",
      "testDateTime": "2021-09-14T07:16:59.706Z",
      "tests": [
        {
          "name": "WHITE BLOOD CELL COUNT",
          "result": "3.9",
          "resultExpected": "3.8-10.8 Thousand/uL",
          "technology": "PHOTOMETRY",
          "method": "GOD-PAP METHOD",
          "description": "CBC ",
          "testParameters": [
            {
              "name": "",
              "value": ""
            }
          ]
        },
        {
          "name": "RED BLOOD CELL COUNT",
          "result": "5.24",
          "resultExpected": "4.20 - 5.80 Million/uL",
          "technology": "PH",
          "method": "GOD-PAP METHOD",
          "description": "CBC TEST",
          "testParameters": [
            {
              "name": "",
              "value": ""
            }
          ]
        },
        {
          "name": "HEMOGLOBIN",
          "result": "16.5",
          "resultExpected": "13.2 - 17.1 g/dL",
          "technology": "PH",
          "method": "GOD-PAP METHOD",
          "description": "CBC ",
          "testParameters": [
            {
              "name": "",
              "value": ""
            }
          ]
        },
        {
          "name": "MCV",
          "result": "94.9",
          "resultExpected": "80.0 - 100.0 fL",
          "technology": "PH",
          "method": "GOD-PAP METHOD",
          "description": "CBC ",
          "testParameters": [
            {
              "name": "Param1",
              "value": "This is to describe more test property"
            }
          ]
        }
      ],
      "description": "desc"
    }
  ]
}
```
The lab report is and patient id is required field for this endpoint. You can update lab report test details as well as patient for lab report.

/labreport/delete: This endpoint can be used to delete lab report already created. Please refer below endpoint and parameter to delete lab report.

**/delete/{reportId}**

The valid report id needs to pass in order to delete a lab report and only lab report will be deleted not the patient.


**/labreport/get:** This endpoint can be used to get list of lab reports. 

**/patient/get/{reportId}:** This endpoint can be used to get specific lab report details by report id. 

### Unit Testing

There is a **Test** folder in the solution having unit test project for each indivisual components, although some of them have not been completed considering short span of time.

