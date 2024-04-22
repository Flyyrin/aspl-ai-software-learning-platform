var chat = []
$(document).ready(function () {
    $('.chat-box').on("keypress", function(e) {
        if (e.keyCode == 13 && !event.shiftKey) {
            var question = $(".chat-box").val();
            if (question.trim() !== '') {
                $(".chat-box").val("");
                addChat("student", question, true)
                addChat("ai", "-", true)
                return false;
            }
        }
    });
});

function getCurrentTime() {
    return new Date().toISOString().slice(0, 19).replace('T', ' ');
}

async function addChat(sender, message, isNew) {
    if (isNew && sender == "student") {
        chat.push({
            sender: sender,
            content: message,
            time: getCurrentTime()
        })
    }

    if (isNew && sender == "ai" && message != "-") {
        chat.push({
            sender: sender,
            content: message,
            time: getCurrentTime()
        })
    }

    var avatar = ""
    var name = ""
    if (sender == "ai") {
        name = "Garry Ai"
        avatar = "garry"
        
    } else if (sender == "student") {
        name = "You - " + studentName
        avatar = "jerro/" + studentAvatar
    }

    var aiClass = ""
    if (sender == "ai" && isNew) {
        aiClass = "w-0"
    }

    var htmlMessage = ""
    $.each(message.split("\n"), function (index, line) {
        htmlMessage += "<p class='mb-2 "+ aiClass +"'>" + line + "</p>"
    });

    if (message == "-" && sender == "ai") {
        htmlMessage = "<div class='dot-typing'></div>"
    }

    var messageTemplate = $(`
    <div class="row mb-4">
        <div class="col-2 p-0 pr-2">
            <img src="img/avatars/${avatar}.png" class="rounded-circle mx-auto d-block w-75">
        </div>
        <div class="col-10 p-0">
            <p class="font-weight-bold mb-0">${name}</p>
            <div class="message-content">
                ${htmlMessage}
            </div>
        </div>
    </div>
    `)
    $(".question-section-content").append(messageTemplate)
  
    scrollToBottom()

    if (sender == "ai" && isNew) {
        if (message == "-") {
            $(".chat-box").prop('disabled', true);
            $(".chat-box").attr('placeholder', "Please wait...");

            var awnser = await getAiResponse()

            var htmlMessage = ""
            $.each(awnser.split("\n"), function (index, line) {
                htmlMessage += "<p class='mb-2 " + aiClass + "'>" + line + "</p>"
            });

            $(messageTemplate).find(".message-content").html(htmlMessage)
            chat.push({
                sender: sender,
                content: awnser,
                time: getCurrentTime()
            })
        } else {

        }

        animateMessage(messageTemplate)
    }

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
    $(".chat-box").attr('placeholder', "Please wait...");
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
        $(".question-nav").attr('data-toggle', "collapse");
        $(".chat-box").prop('disabled', false);
        $(".chat-box").attr('placeholder', "Message...");
    }, (elements.length * 2000) + 100);
}

function resizeMessageContainer(messageContainer) {
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

function loadChat() {
    $.ajax({
        url: 'app/getChat',
        method: 'GET',
        data: {
            course: currentCourse
        },
        success: function (response) {
            chat = response["messages"]

            if (chat.length <= 0) {
                addChat("ai", "👋 Welcome, i am Garry!\nNeed help with your courses, code, output, or errors ? I've got you covered!\n📘 Courses: I'll guide you through your courses.\n💻 Code: Stuck on code ? Let's debug it together.\n🔍 Output: Confused about output ? I'll decode it for you.\n❌ Error: Hit an error ? I'll help you fix it.\nJust ask, and I'll be here to assist you every step of the way! 🚀", true)
            } else {
                $.each(chat, function (index, message) {
                    addChat(message.sender, message.content, false)
                });
            }
            console.log("Loaded Chat")
        },
        error: function (xhr, status, error) {
            alertMessage("Something Went Wrong!")
        }
    });
}

async function getAiResponse() {
    await delay(5000)
    return "Awnser"
}

function delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}