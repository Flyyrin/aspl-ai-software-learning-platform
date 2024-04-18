var chat = []
$(document).ready(function () {
    addAiChat("👋 Welcome, i am Garry!\nNeed help with your courses, code, output, or errors ? I've got you covered!\n📘 Courses: I'll guide you through your courses.\n💻 Code: Stuck on code ? Let's debug it together.\n🔍 Output: Confused about output ? I'll decode it for you.\n❌ Error: Hit an error ? I'll help you fix it.\nJust ask, and I'll be here to assist you every step of the way! 🚀")

    $('.chat-box').on("keypress", function(e) {
        if (e.keyCode == 13 && !event.shiftKey) {
            var question = $(".chat-box").val();
            if (question.trim() !== '') {
                $(".chat-box").val("");
                addStudentChat(question)
                addAiChat("Awnser")
                return false;
            }
        }
    });
});

function addStudentChat(message) {
    chat.push({
        sender: "student",
        content: message
    })
    var htmlMessage = ""
    $.each(message.split("\n"), function (index, line) {
        htmlMessage += "<p class='mb-2'>"+ line +"</p>"
    });

    var messageTemplate = `
    <div class="row mb-4">
        <div class="col-2 p-0 pr-2">
            <img src="img/avatars/jerro/${studentAvatar}.png" class="rounded-circle mx-auto d-block w-75">
        </div>
        <div class="col p-0">
            <p class="font-weight-bold mb-0">You - ${studentName}</p>
            <div class="message-content">
                ${htmlMessage}
            </div>
        </div>
    </div>
    `
    $(".question-section-content").append(messageTemplate)
    scrollToBottom()
}

function addAiChat(message) {
    chat.push({
        sender: "ai",
        content: message
    })
    var htmlMessage = ""
    $.each(message.split("\n"), function (index, line) {
        htmlMessage += "<p class='mb-2 w-0'>" + line + "</p>"
    });

    var messageTemplate = $(`
    <div class="row mb-4">
        <div class="col-2 p-0 pr-2">
            <img src="img/avatars/garry.png" class="rounded-circle mx-auto d-block w-75">
        </div>
        <div class="col-10 p-0">
            <p class="font-weight-bold mb-0">Garry AI</p>
            <div class="message-content">
                ${htmlMessage}
            </div>
        </div>
    </div>
    `)
    $(".question-section-content").append(messageTemplate)
    animateMessage(messageTemplate)
    scrollToBottom()
    saveChat()
}

function scrollToBottom() {
    var chat = $('.question-section-content');
    chat.scrollTop(chat[0].scrollHeight);
}

function animateMessage(message) {
    if (!$("#question-section-content").hasClass("show")) {
        $(".question-nav").trigger('click');
    }
    $(".question-nav").attr('data-toggle', "");
    $(".chat-box").prop('disabled', true);
    $(".chat-box").attr('placeholder', "Wait...");
    var elements = $(message).find(".message-content").children();
    var messageContainer = $(message).find(".message-content")
    elements.each(function (index) {
        var element = $(this);
        setTimeout(function () {
            element.addClass('typing');
            element.removeClass('w-0');
            resizeMessageContainer(messageContainer)
        }, index * 2000);

        setTimeout(function () {
            resizeMessageContainer(messageContainer)
        }, (index * 2000) + 100);

        setTimeout(function () {
            element.removeClass('typing');
        }, (index * 2000) + 2100);
    });

    setTimeout(function () {
        resizeMessageContainer(messageContainer)
    }, (elements.length * 2000) + 100);
}

function resizeMessageContainer(messageContainer) {
    $(".question-nav").attr('data-toggle', "collapse");
    $(".chat-box").prop('disabled', false);
    $(".chat-box").attr('placeholder', "Message...");

    var totalOuterHeight = 0;
    $(messageContainer).children().each(function () {
        if ($(this).width() !== 0) {
            totalOuterHeight += $(this).outerHeight(true);
        }
    });
    $(messageContainer).outerHeight(totalOuterHeight);
    scrollToBottom()
}

function saveChat() {
    console.log(chat)
    $.ajax({
        url: 'app/saveChat',
        method: 'POST',
        data: {
            course: currentCourse,
            chat: chat
        },
        success: function (response) {
            console.log("Saved Chat")
        },
        error: function (xhr, status, error) {
            alertMessage("Something Went Wrong!")
        }
    });
}