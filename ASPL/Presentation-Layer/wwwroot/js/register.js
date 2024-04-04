$(document).ready(function(){
    $(".restrictions").hide()

    $("input[name='username']").on("input", function() {
        validateUsername();
    });

    $("input[name='email']").on("input", function() {
        validateEmail();
    });

    $("input[name='password']").on("input", function() {
        validatePassword();
    });

    $("input[name='passwordRepeat']").on("input", function() {
        validatePasswordRepeat();
    });

    $(".btn").on('click', function(e) {
        if (!validateForm()) {
            e.preventDefault();  
            alert("prevent")
            validateUsername()
            validateEmail()
            validatePassword()
            validatePasswordRepeat()
        }
    });
});

function validateForm() {
    return validateUsername() && validateEmail() && validatePassword() && validatePasswordRepeat();
}

function validateUsername() {
    var username = $("input[name='username']")
    var regex = /^[a-zA-Z0-9]+$/;

    if (username.val().length == 0) {
        $(username).addClass("invalid")
        $(".usernameMessage").text("Username Cannot Be Empty.")
        return false
    } else if (!regex.test(username.val())) {
        $(username).addClass("invalid")
        $(".usernameMessage").text("Username Contains Invalid Characters.")
        return false
    } else if (username.val().length > 10 ) {
        $(username).addClass("invalid")
        $(".usernameMessage").text("Username Is To Long.")
        return false
    } else {
        $(username).removeClass("invalid")
        $(".usernameMessage").text("")
        return true
    }
}

function validateEmail() {
    var email = $("input[name='email']")
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!emailRegex.test(email.val())) {
        $(email).addClass("invalid")
        $(".emailMessage").text("Invalid Email.")
        return false
    } else {
        $(email).removeClass("invalid")
        $(".emailMessage").text("")
        return true
    }
}

function validatePassword() {
    validatePasswordRepeat();
    var password = $("input[name='password']")
    var passwordRegex = /^(?=.*[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?])(?=.*[0-9])(?!\s).{5,}$/;
    var hasNumberRegex = /\d/;
    var hasSpecialCharRegex = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/;
    var hasSpaceRegex = /\s/;

    if (!passwordRegex.test(password.val())) {
        $(".restrictions").show()
        $(password).removeClass("mb-3")
        $(password).addClass("invalid")
        $(".passwordMessage").text("Invalid Password.")
        
        if (password.val().length < 5) {
            $(".restrictionLength").show()
        } else {
            $(".restrictionLength").hide()
        }
        
        if (hasSpaceRegex.test(password.val())) {
            $(".restrictionSpaces").show()
        } else {
            $(".restrictionSpaces").hide()
        }

        if (!hasSpecialCharRegex.test(password.val())) {
            $(".restrictionSpecialChar").show()
        } else {
            $(".restrictionSpecialChar").hide()
        }
        
        if (!hasNumberRegex.test(password.val())) {
            $(".restrictionNumber").show()
        } else {
            $(".restrictionNumber").hide()
        }
    
    } else {
        $(".restrictions").hide()
        $(password).addClass("mb-3")
        $(password).removeClass("invalid")
        $(".passwordMessage").text("")
        return true
    }
}

function validatePasswordRepeat() {
    var password = $("input[name='password']")
    var passwordRepeat = $("input[name='passwordRepeat']")

    if (passwordRepeat.val().length != 0) {
        if (password.val() != passwordRepeat.val()) {
            $(passwordRepeat).addClass("invalid")
            $(".passwordRepeatMessage").text("Passwords Do Not Match.")
            return false
        } else {
            $(passwordRepeat).removeClass("invalid")
            $(".passwordRepeatMessage").text("")
            return true
        }
    }
}