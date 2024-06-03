var currentCourse = localStorage.getItem("lastCourse");
var currentChapter = localStorage.getItem("lastChapter");
var menuOpen1 = localStorage.getItem("menuOpen1");
var menuOpen2 = localStorage.getItem("menuOpen2");
var spinner = "";
var cooldownTime = 3000;
var courseName = "";

if (currentCourse === null) {
    currentCourse = 1;
    localStorage.setItem("lastCourse", currentCourse);
}

if (currentChapter === null) {
    currentChapter = 1;
    localStorage.setItem("lastChapter", currentChapter);
}

if (menuOpen1 === null) {
    menuOpen1 = false;
    localStorage.setItem("menuOpen1", menuOpen1);
}

if (menuOpen2 === null) {
    menuOpen2 = false;
    localStorage.setItem("menuOpen2", menuOpen2);
}

if (currentCourse == 1) { courseName = "python" }
if (currentCourse == 2) { courseName = "csharp" }
if (currentCourse == 3) { courseName = "javascript" }


$(document).ready(function () {
    if (chapterMenu == "open") {
        setTimeout(function () {
            $(".bi-list").trigger("click")
            setTimeout(function () {
                $('.chapterDropdown').scrollTop(9999999);
            }, 500);
        }, 500);
    }
    if (chapterMenu == "open2") {
        setTimeout(function () {
            $(".bi-list").trigger("click")
        }, 500);
    }
    if (menuOpen1 == "true") {
        $(".question-nav").trigger("click")
    }
    if (menuOpen2 == "true") {
        $(".explaination-nav").trigger("click")
    }

    $('.question-nav').on('click', function () {
        if ($("#question-section-content").hasClass("show")) {
            menuOpen1 = false;
        } else {
            menuOpen1 = true;
        }
        localStorage.setItem("menuOpen1", menuOpen1);
    })

    $('.explaination-nav').on('click', function () {
        if ($("#explaination-section-content").hasClass("show")) {
            menuOpen2 = false;
        } else {
            menuOpen2 = true;
        }
        localStorage.setItem("menuOpen2", menuOpen2);
    })

    $('#question-section-content').on('show.bs.dropdown', function () {
        menuOpen1 = false;
        localStorage.setItem("menuOpen1", menuOpen1);
    })

    spinner = $("#course-content").html()
    loadChapters()

    $("#course-dropdown a").on("click", function (event) {
        var newCourse = parseInt($(this).attr("course"));
        if (newCourse != currentCourse) {
            $(this).parent().find(".bi").removeClass("bi-check-circle").addClass("bi-circle")
            $(this).find(".bi").removeClass("bi-circle").addClass("bi-check-circle")
            $("#course-name").text($(this).find("#list-course-name").text())
            $("#course-image").attr("src", $(this).find("img").attr("src"))
            
            currentCourse = newCourse
            localStorage.setItem("lastCourse", currentCourse);
            Cookies.set('lastCourse', currentCourse.toString());
            currentChapter = 1;
            localStorage.setItem("lastChapter", currentChapter);
            loadChapters()
        }
    });

    $("#chapter-next").on("click", function () {
        if (currentChapter < parseInt($("#chapter-count").text())) {
            currentChapter++;
            localStorage.setItem("lastChapter", currentChapter);
            $("#current-chapter").text(currentChapter)
            $("#chapter-dropdown").find("a").removeClass("selected")
            $("#chapter-dropdown").find("a").eq(currentChapter-1).addClass("selected")
            loadContent()
        }
    });

    $("#chapter-prev").on("click", function () {
        if (currentChapter > 1) {
            currentChapter--;
            localStorage.setItem("lastChapter", currentChapter);
            $("#current-chapter").text(currentChapter)
            $("#chapter-dropdown").find("a").removeClass("selected")
            $("#chapter-dropdown").find("a").eq(currentChapter - 1).addClass("selected")
            loadContent()
        }
    });
});

function loadContent() {
    last_chapter = currentChapter
    alertMessage("Loading Content...")
    $(".question-section-content").empty()
    $("#course-content").html(spinner)
    loadChapterContent()
    resizeCourse()
    loadCode()
    loadChat()
    setTimeout(function () {
        hljs.highlightAll();
        loadCodeSnippet()
    }, 1000);
}

function loadChapters() {
    $.ajax({
        url: 'app/getChapters',
        data: { course: currentCourse },
        type: 'GET',
        success: function (response) {
            $("#chapter-count").text(response.length)
            $("#current-chapter").text(currentChapter)
            $("chapter-dropdown").find("a").removeClass("selected")
            $('#chapter-dropdown').empty()
            $.each(response, function (index, chapter) {
                var newChapterItem = chapterItem
                newChapterItem = newChapterItem.replace("{{chapter_id}}", chapter.id)
                newChapterItem = newChapterItem.replace("{{chapter_name}}", chapter.name)
                newChapterItem = newChapterItem.replace("{{chapter_description}}", chapter.description)
                newChapterItem = newChapterItem.replace("{{chapter_name}}", chapter.name)
                newChapterItem = newChapterItem.replace("{{chapter_description}}", chapter.description)
                if (index + 1 == currentChapter) {
                    newChapterItem = newChapterItem.replace("var-placeholder", "selected")
                }
        
                $('#chapter-dropdown').append(newChapterItem);
            });

            loadContent()
            enableEdit()
            enableDelete()

            $("#chapter-dropdown a").on("click", function (event) {
                var newChapter = parseInt($(this).index())+1;
                if (newChapter != currentChapter) {
                    $(this).siblings().removeClass("selected")
                    $(this).addClass("selected")

                    currentChapter = newChapter
                    localStorage.setItem("lastChapter", currentChapter);
                    $("#current-chapter").text(currentChapter)
                    loadContent()
                }
            });
        },
        error: function (xhr, status, error) {
            alertMessage("Something Went Wrong!")
        }
    });
}

function loadCodeSnippet() {
    if ($("#course-content").find("pre").prev(".snippet-header").length === 0) {
        $("#course-content").find("pre").before('<div class="snippet-header d-flex py-2 px-3 justify-content-end"><h3 class="clickable m-0 mr-3" id="snippetCopyCode"><i class="bi bi-clipboard" data-toggle="tooltip" data-placement="top" title="Copy code"></i><i class="bi bi-check2"></i></h3><h3 class="clickable m-0" id="snippetCopyCodeEditor"><i class="bi bi-code-slash" data-toggle="tooltip" data-placement="top" title="Copy to editor"></i><i class="bi bi-check2"></i></h3></div>')
    }

    $("#snippetCopyCode .bi-check2").hide();
    $("#course-content").on("click", "#snippetCopyCode", function () {
        $(this).find(".bi-check2").show();
        $(this).find(".bi-clipboard").hide();

        navigator.clipboard.writeText($(this).parent().next().find("code").text());
        alertMessage("Copied Code!")

        setTimeout(function () {
            $("#snippetCopyCode .bi-check2").hide();
            $("#snippetCopyCode .bi-clipboard").show();
            saveCode()
        }, 2000);
    });

    $("#snippetCopyCodeEditor .bi-check2").hide();
    $("#course-content").on("click", "#snippetCopyCodeEditor", function () {
        $(this).find(".bi-check2").show();
        $(this).find(".bi-code-slash").hide();

        editor.setValue($(this).parent().next().find("code").text())
        alertMessage("Copied Code To Editor!")

        setTimeout(function () {
            $("#snippetCopyCodeEditor .bi-check2").hide();
            $("#snippetCopyCodeEditor .bi-code-slash").show();
            saveCode()
        }, 2000);
    });
}

function loadChapterContent() {
    var chapterId = $('#chapter-dropdown .selected').attr('chapter');
    $.ajax({
        url: 'app/getChapter',
        data: { chapter: chapterId },
        type: 'GET',
        success: function (response) {
            console.log(response)
            var code = decodeCode(response.content)

            var button = ""
            if (studentRole == "admin") {
                button = `<a class="btn btn-el text-white w-auto d-block mt-3 mb-3" href="/editor/${response.id}">Edit content</a>`
            }

            $("#course-content").html(button+code);
            alertMessage("Loaded chapter content.")
        },
        error: function (xhr, status, error) {
            alertMessage("Something Went Wrong!")
        }
    });
}