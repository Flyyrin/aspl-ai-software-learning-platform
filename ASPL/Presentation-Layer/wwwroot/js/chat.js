var chat = []
var lastMessage = "";
var lastChatSave = new Date();
$(document).ready(function () {
    $('.chat-box').on("keypress", function(e) {
        if (e.keyCode == 13 && !event.shiftKey) {
            var question = $(".chat-box").val();
            if (question.trim() !== '') {
                $(".chat-box").val("");
                addChat("student", question, true)
                lastMessage = question;
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
    var inCode = false;
    var messageContent = []
    $.each(message.split("```"), function (index, part) {

        if (part != "") {
            if (inCode) {
                messageContent.push({
                    "type": "code",
                    "content": part
                })
            } else {
                messageContent.push({
                    "type": "text",
                    "content": part
                })
            }
        }
        inCode = !inCode;
    });

    messageContentFormatted = []
    var firstH4 = true;

    $.each(messageContent, function (index, item) {
        if (item.type == "text") {
            if (item.content.includes('\n')) {
                $.each(item.content.split("\n"), function (index, line) {
                    if (line != "") {
                        line = correctMessage(line)

                        line = line.replace(/\*\s\*\*(.*?)\*\*/g, function (match, group1) {
                            return "• " + group1
                        });
                        line = line.replace(/\*\*(.*?)\*\*/g, function (match, group1) {
                            if (firstH4) {
                                firstH4 = false
                                return "<h4 class='mt-0 " + aiClass + "'>" + group1 + "</h4>";
                            } else {
                                return "<h4 class='mt-4 " + aiClass + "'>" + group1 + "</h4>";

                            }
                        });
                        line = line.replace(/(<\/h4>)(.*?)(?=<h4|$)/g, function (match, h4End, afterH4) {
                            return h4End + "<p>" + afterH4 + "</p>";
                        });
                        line = line.replace(/\*\s(\w+)/g, "• $1");

                        if (line.includes("<h4>")) {
                            htmlMessage += line
                        } else {
                            htmlMessage += "<p class='mb-2 " + aiClass + "'>" + line + "</p>";
                        }
                    }
                });
            } else {
                htmlMessage += "<p class='mb-2 " + aiClass + "'>" + item.content + "</p>";
            }
        } else {
            const highlightedCode = hljs.highlight(
                item.content,
                { language: "python" }
            ).value

            htmlMessage += "<pre><div><code>" + highlightedCode + "\n</code></div></pre>";
        }
    });

    $(messageTemplate).find(".message-content").html(htmlMessage)

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

            var awnser = await getResponse()
            var htmlMessage = ""
            var inCode = false;
            var messageContent = []
            $.each(awnser.split("```"), function (index, part) {

                if (part != "") {
                    if (inCode) {
                        messageContent.push({
                            "type": "code",
                            "content": part
                        })
                    } else {
                        messageContent.push({
                            "type": "text",
                            "content": part
                        })
                    }
                }
                inCode = !inCode;
            });

            messageContentFormatted = []
            var firstH4 = true;
            $.each(messageContent, function (index, item) {
                if (item.type == "text") {
                    if (item.content.includes('\n')) {
                        $.each(item.content.split("\n"), function (index, line) {
                            if (line != "") {
                                line = correctMessage(line)
                                line = line.replace(/\*\s\*\*(.*?)\*\*/g, function (match, group1) {
                                    return "• " + group1
                                });
                                line = line.replace(/\*\*(.*?)\*\*/g, function (match, group1) {
                                    if (firstH4) {
                                        firstH4 = false
                                        return "<h4 class='mt-0 " + aiClass + "'>" + group1 + "</h4>";
                                    } else {
                                        return "<h4 class='mt-4 " + aiClass + "'>" + group1 + "</h4>";

                                    }
                                });
                                line = line.replace(/(<\/h4>)(.*?)(?=<h4|$)/g, function (match, h4End, afterH4) {
                                    return h4End + "<p class='" + aiClass + "'>" + afterH4 + "</p>";
                                });
                                line = line.replace(/\*\s(\w+)/g, "• $1");

                                if (line.includes("<h4>")) {
                                    htmlMessage += line
                                } else {
                                    htmlMessage += "<p class='mb-2 " + aiClass + "'>" + line + "</p>";
                                }
                            }
                        });
                    } else {
                        htmlMessage += "<p class='mb-2 " + aiClass + "'>" + item.content + "</p>";
                    }
                } else {
                    const highlightedCode = hljs.highlight(
                        item.content,
                        { language: "python" }
                    ).value

                    htmlMessage += "<pre class='hiddenCode'><div class='w-0'><code>" + highlightedCode + "\n</code></div></pre>";
                }
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

        if (element.is("p") || element.is("h4")) {
            if (element.text() != "") {
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
            }
        } else {
            setTimeout(function () {
                element.removeClass("hiddenCode")
                resizeMessageContainer(messageContainer)
            }, index * 2000);

            var codeElement = element.find(".w-0");
            setTimeout(function () {
                codeElement.addClass('codeTyping');
                codeElement.removeClass('w-0');
                resizeMessageContainer(messageContainer)
            }, index * 2000);

            setTimeout(function () {
                resizeMessageContainer(messageContainer)
            }, (index * 2000) + 100);

            setTimeout(function () {
                codeElement.removeClass('codeTyping');
            }, (index * 2000) + 2100);
        }
    });

    setTimeout(function () {
        resizeMessageContainer(messageContainer)
        $(".question-nav").attr('data-toggle', "collapse");
        $(".chat-box").prop('disabled', false);
        $(".chat-box").attr('placeholder', "Message...");
        $(".chat-box").focus();
    }, (elements.length * 2000) + 100);
}

function resizeMessageContainer(messageContainer) {
    var totalOuterHeight = 0;
    $(messageContainer).children().each(function () {
        if ($(this).width() !== 0 && !$(this).hasClass("hiddenCode")) {
            totalOuterHeight += $(this).outerHeight(true);
        }
    });
    $(messageContainer).outerHeight(totalOuterHeight);
    scrollToBottom()
}

function saveChat() {
    var currentTime = new Date()
    if (Math.abs(lastChatSave - currentTime) < 1000) {
        console.log("Skipped Chat Save");
        return;
    }
    lastChatSave = new Date();
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

function correctMessage(message) {
    message = message.replace(/`(.*?)`/g, function (match, group1) {
        return "<code>" + group1 + "</code>";
    });

    return message
}