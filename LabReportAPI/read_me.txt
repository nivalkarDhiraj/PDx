

LAB Report API has been created as part of HCA Software development organization's, API development challenges only.

In order to executed the code avaiable, pelase launch the solution file - LabReportAPI.sln with Visual studio 2019 + .Net 5.0 target framework.
And make sure the dependencies are updated/reffered good, as requires the following to build/execute/publishing.
   1. Microsoft.EntityFrameworkCore.Sqllite(5.0.9)
   2. Swashbuckle.AspNetCore(6.2.1)

## UNDERSTANDING PROBLEM STATEMENT:
/*****************************************************************************/

Create an API with .Net core with C#, API should make use of any application, so the end user should be able to create/update/delete/select Member information and Lab report information.


## SOLUTION:
/*****************************************************************************/
   ## FUNCTIONAL OVERVIEW:

   As a Healthcare business process, MD should be able to check for member existance in the system and suggest for diagnosis to treat a patient.
   Labaratory should be able pull the MD's suggestion and deriver unit of messument based on the same recieved from the patient. And Lab report can be updated back to the system.

      a. Patient details can be add/update/delete/selected using the member SSN as a Key or subscriber id with member relationship code.
      b. MD should be able to pull member record, and Add/Update/Delete/Extract MD note details with suggested diagnosis informations with patient visit id.
      C. Lab technition should be abele to Add/Edit/Delete/Search Lab report based on the MD note details added at (b).
            i. Searches can be performed with visit_id = report_id
           ii. Searches can be performed with test id added to the system
          iii. Searches can be performed with type of test performed against the date range in which the test samples are provided.

   ## ASSUMPTION:
      a. Member eligiblity assumed to eligibility as and when the member info available with the system.
      b. Provider info and Network details are configured, so the MD suggestion are tracked only with provider id alone.

   ## TECHNICAL OVERVIEW:

      a. As a end-user, should be abel to perform any CRUD operation with .Net Core API with sql lite DB for the above give functional overview.
      b. Internal Member cache is added to perform the search operation to featch Member/MD-Note/Lab report details, to bypass the DB calls.
      c. All connection string and cache exipiration as configured to access it from appsettings.json file.
      d. Any unexpected exception are logged to server event-viewer for trubleshooting purpose.



##TESTING PROCEDURE:
/*****************************************************************************/

1. Create a Member information with valid SBSB_ID & relationship code, Sample below: And the same should be able to perform with other CRUD operations with sql lite db and member caching used with API.

[
  {
    "sbsb_id": 999990,
    "meme_rel": "SELF",
    "meme_first_name": "Mr Test",
    "meme_last_name": "User",
    "meme_dbo": "1970-01-01T12:19:36.111",
    "meme_gender": "MALE",
    "meme_ssn": 12341200,
    "meme_mail_id": "TEST.USER@HCA.COM"
  }]


2. Create a physician visit for the member enrolled with valid visit id, sample below:

{
  "visit_id": 11,
  "meme_ssn": 12341200,
  "provider_id": 9000001,
  "visit_dtm": "2021-09-16T11:56:12.181Z",
  "md_diag_suggest": "Blood group (A,B,O); Rh-Type; Total protein; Bilirubin;",
  "md_note": "Identified with following symptoms and awating on lab report on the diagnosis details suggested. Symptom 1: High body temparature, Symptom 2: Weak bones and coughing Symtom 3: Yellow eye-ball & skin"
}

3. Create an Lab report details based on the Visit id with valid report id and test id's, sample below:

[ {
    "diag_test_id": 11,
    "visit_id": 11,
    "diag_type_name": "Blood group (A,B,O)",
    "diag_sample_dtm": "2021-09-16T12:06:18.370Z",
    "diag_unit_messured": "NA",
    "diag_result": "B",
    "diag_result_time": "2021-09-14T06:06:18.370Z"
  },
  {
    "diag_test_id": 12,
    "visit_id": 11,
    "diag_type_name": "RH TYPE",
    "diag_sample_dtm": "2021-09-14T12:06:18.370Z",
    "diag_unit_messured": "NA",
    "diag_result": "+VE",
    "diag_result_time": "2021-09-14T06:06:18.370Z"
  },
  {
    "diag_test_id": 13,
    "visit_id": 11,
    "diag_type_name": "Bilirubin",
    "diag_sample_dtm": "2021-09-14T12:06:18.370Z",
    "diag_unit_messured": "1.0 mg/dL",
    "diag_result": "-VE",
    "diag_result_time": "2021-09-14T06:06:18.370Z"
  }]

/*****************************************************************************/