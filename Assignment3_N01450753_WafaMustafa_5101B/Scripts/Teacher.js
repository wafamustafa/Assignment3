// AJAX for teacher add and update

//function for validating teachers info
function ValidateTeacher() {

	var IsValid = true;
	var ErrorMsg = "";
	var ErrorBox = document.getElementById("ErrorBox");
	var teacherFname = document.getElementById('teacherFname').value;
	var teacherLname = document.getElementById('teacherLname').value;
	var employeeNumber = document.getElementById('employeeNumber').value;
	

	//First Name is three or more characters
	if (teacherFname.length < 3) {
		IsValid = false;
		ErrorMsg += "Teacher's First name should be more than 3 letters.<br>";
	}
	//Last Name is three or more characters
	if (teacherLname.length < 3) {
		IsValid = false;
		ErrorMsg += "Teacher's Last name should be more than 3 letters.<br>";
	}
	//Employee number is more than 2 numbers 
	if (employeeNumber.length < 2) {
		IsValid = false;
		ErrorMsg += "Please Enter a valid Employee Number.<br>";
	}

	if (!IsValid) {
		ErrorBox.style.display = "block";
		ErrorBox.innerHTML = ErrorMsg;
	} else {
		ErrorBox.style.display = "none";
		ErrorBox.innerHTML = "";
	}

	return IsValid;
}

//check for validation for adding teachers... referaence code from blog project 7
function AddTeacher() {

	//check for validation 
	var IsValid = ValidateTeacher();
	if (!IsValid) return;

	
	var URL = "http://localhost:63138/api/TeacherData/AddTeacher";

	var rq = new XMLHttpRequest();
	

	var teacherFname = document.getElementById('teacherFname').value;
	var teacherLname = document.getElementById('teacherLname').value;
	var employeeNumber = document.getElementById('employeeNumber').value;


	var TeacherData = {
		"teacherFname": teacherFname,
		"teacherLname": teacherLname,
		"employeeNumber": employeeNumber	
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}

//Validation for add teacher was added to views. I know im doing something worng because validation didnt work on the add page not sure where im going worng 







/*/check for validation for updating teachers by id... referaence code from blog project 7
function UpdateTeacher(teacherId) {

	//check for validation 
	var IsValid = ValidateTeacher();
	if (!IsValid) return;


	var URL = "http://localhost:63138/api/TeacherData/UpdateTeacher/{id}";

	var rq = new XMLHttpRequest();


	var teacherFname = document.getElementById('teacherFname').value;
	var teacherLname = document.getElementById('teacherLname').value;
	var employeeNumber = document.getElementById('employeeNumber').value;


	var TeacherData = {
		"teacherFname": teacherFname,
		"teacherLname": teacherLname,
		"employeeNumber": employeeNumber
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}*/

