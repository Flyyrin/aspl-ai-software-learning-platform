var currentCourse = localStorage.getItem("lastCourse");
var currentChapter = localStorage.getItem("lastChapter");
var menuOpen1 = localStorage.getItem("menuOpen1");
var menuOpen2 = localStorage.getItem("menuOpen2");
var spinner = "";
var cooldownTime = 3000;

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

$(document).ready(function () {
    if (menuOpen1 == "true") {
        $(".question-nav").trigger("click")
    }
    if (menuOpen2 == "true") {
        $(".explaination-nav").trigger("click")
    }

    $('.question-nav').on('click', function () {
        console.log("click")
        if ($("#question-section-content").hasClass("show")) {
            menuOpen1 = false;
        } else {
            menuOpen1 = true;
        }
        localStorage.setItem("menuOpen1", menuOpen1);
    })

    $('.explaination-nav').on('click', function () {
        console.log("click")
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
        console.log("next")
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
        console.log("next")
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
    $("#course-content").html(spinner)
    $("#course-content").load("../content/" + currentCourse + "-" + currentChapter + ".html");
    resizeCourse()
    loadCode()
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
                var chapterItem = '<a class="dropdown-item wrap my-2 py-2 var-placeholder" chapter="' + chapter.id + '"><p class="mb-0">' + chapter.name + '</p><p class="mb-0 chapter-description">' + chapter.description + '</p></a>'

                if (index + 1 == currentChapter) {
                    chapterItem = chapterItem.replace("var-placeholder", "selected")
                }
        
                $('#chapter-dropdown').append(chapterItem);
            });

            loadContent()

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