var avatarBody = parseInt(studentAvatar.split("-")[0])
var avatarFace = parseInt(studentAvatar.split("-")[1])
var avatarHat = parseInt(studentAvatar.split("-")[2])

var previewAvatarBody = avatarBody
var previewAvatarFace = avatarFace
var previewAvatarHat = avatarHat

var avatarBodies = 4
var avatarFaces = 3
var avatarHats = 3

$(document).ready(function () {
    loadAvatarPreview()

    $("#avatar-save").on("click", function () {
        saveAvatar()
    });

    $('#avatarModal').on('show.bs.modal', function (e) {
        loadAvatarPreview()
    })

    $("#avatarContainer").on("click", "#avatar-control", function () {
        var part = $(this).attr("part")
        var action = $(this).attr("action")

        if (part == "body") {
            if (action == "prev") {
                if (previewAvatarBody - 1 >= 1) {
                    previewAvatarBody -= 1
                }
            }
            if (action == "next") {
                if (previewAvatarBody + 1 <= avatarBodies) {
                    previewAvatarBody += 1
                }
            }
        } else if (part == "face") {
            if (action == "prev") {
                if (previewAvatarFace - 1 >= 0) {
                    previewAvatarFace -= 1
                }
            }
            if (action == "next") {
                if (previewAvatarFace + 1 <= avatarFaces) {
                    previewAvatarFace += 1
                }
            }
        } else if (part == "hat") {
            if (action == "prev") {
                if (previewAvatarHat - 1 >= 0) {
                    previewAvatarHat -= 1
                }
            }
            if (action == "next") {
                if (previewAvatarHat + 1 <= avatarHats) {
                    previewAvatarHat += 1
                }
            }
        }
        previewAvatar()
    });
});

function previewAvatar() {
    $("#avatarBody").attr("src", getAvatarSrc("bodies", previewAvatarBody))
    $("#avatarFace").attr("src", getAvatarSrc("faces", previewAvatarFace))
    $("#avatarHat").attr("src", getAvatarSrc("hats", previewAvatarHat))
    $("#avatarPreview").attr("src", "img/avatars/jerro/" + previewAvatarBody + "-" + previewAvatarFace + "-" + previewAvatarHat + ".png")
}

function loadAvatarPreview() {
    previewAvatarBody = avatarBody
    previewAvatarFace = avatarFace
    previewAvatarHat = avatarHat
    $("#avatarBody").attr("src", getAvatarSrc("bodies", previewAvatarBody))
    $("#avatarFace").attr("src", getAvatarSrc("faces", previewAvatarFace))
    $("#avatarHat").attr("src", getAvatarSrc("hats", previewAvatarHat))
    $("#avatarPreview").attr("src", "img/avatars/jerro/" + previewAvatarBody + "-" + previewAvatarFace + "-" + previewAvatarHat + ".png")
}

function loadAvatar() {
    $("#avatar").attr("src", "img/avatars/jerro/" + avatarBody + "-" + avatarFace + "-" + avatarHat + ".png")
}

function saveAvatar() {
    var newStudentAvatar = previewAvatarBody + "-" + previewAvatarFace + "-" + previewAvatarHat
    if (newStudentAvatar != studentAvatar) {
        console.log("save")
        avatarBody = previewAvatarBody
        avatarFace = previewAvatarFace
        avatarHat = previewAvatarHat
        studentAvatar = newStudentAvatar

        $.ajax({
            url: 'app/saveAvatar',
            method: 'POST',
            data: {
                avatar: studentAvatar
            },
            success: function (response) {
                loadAvatar()
                alertMessage("Saved avatar!")
            },
            error: function (xhr, status, error) {
                alertMessage("Something Went Wrong!")
            }
        });
    }
}

function getAvatarSrc(part, number) {
    return "img/avatars/"+ part +"/"+ number  +".png"
}