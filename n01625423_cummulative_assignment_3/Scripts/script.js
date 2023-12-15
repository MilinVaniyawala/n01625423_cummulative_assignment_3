// Created Validation Function To validate teach form fields.
window.onload = function () {
    // getting form from html
    var teacherForm = document.forms["updateTeacherForm"];

    var fname = teacherForm["TeacherFname"];
    var lname = teacherForm["TeacherLname"];
    var empNumber = teacherForm["EmployeeNumber"];
    var salary = teacherForm["Salary"];

    // Submit btn
    var submitBtn = document.getElementById("updateTeacher");
    // event listener on btn
    submitBtn.addEventListener('click', (event) => {
        // to stop the submit
        event.preventDefault();

        // validate First Name
        if (fname.value === "") {
            alert("Firstname field is empty");
            return false;
        }
        // Validate Last Name
        if (lname.value === "") {
            alert("Lastname field is empty");
            return false;
        }
        // Validate Employee Number
        if (empNumber.value === "") {
            alert("Employee number field is empty");
            return false;
        }

        // Validate Salary
        var salaryVal = parseFloat(salary.value);
        if (isNaN(salaryVal)) {
            alert("Invalid Value!!");
            return false;
        }
        // salary can't be in minus
        if (salary.value < 0) {
            alert("Salary field can not have value in negative. Please enter again");
            return false;
        }

        // if all validation are passed
        teacherForm.submit();
    });
}