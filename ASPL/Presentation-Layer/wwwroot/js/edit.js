var deleteChapterInt;
var nextChapterIdex;

var allowedElements = {
    text:     '<p>{{content}}</p>',
    title:    '<h1 class="mt- 0">{{content}}</h1>',
    subtitle: '<h2 class="mt- 0">{{content}}</h2>',
    header:   '<h3 class="mt- 0">{{content}}</h3>',
    snippet:  '<pre><code class="language-python my-3">{{content}}</code></pre>',
}
    
$(document).ready(function () {
    var code = editor.getValue()
    code = decodeCode(code)
    $("#output-content").html(code)
    hljs.highlightAll();

    $("#addTitleButton").on("click", function (event) {
        var code = editor.getValue()
        code += '\n<title>New title</title>\n'
        editor.setValue(code)
    });

    $("#addSubTitleButton").on("click", function (event) {
        var code = editor.getValue()
        code += '\n<subtitle>New subtitle</subtitle>\n'
        editor.setValue(code)
    });

    $("#addHeaderButton").on("click", function (event) {
        var code = editor.getValue()
        code += '\n<header>New header</header>\n'
        editor.setValue(code)
    });

    $("#addTextButton").on("click", function (event) {
        var code = editor.getValue()
        code += "\n<text>New text</text>\n"
        editor.setValue(code)
    });

    $("#addSnippetButton").on("click", function (event) {
        var code = editor.getValue()
        code += `\n<snippet># New snippet</snippet>\n`
        editor.setValue(code)
    });

    $("#addTagButton").on("click", function (event) {
        var code = editor.getValue()
        code += `((New tag))`
        editor.setValue(code)
    });
    
    editor.on('change', (args) => {
        $("#saveIcon").removeClass("bi-cloud-check").addClass("bi-cloud-minus")
        var code = editor.getValue()
        code = decodeCode(code)
        $("#output-content").html(code)
        hljs.highlightAll();
    })

    $("#saveButton").on("click", function (event) {
        saveChapter(chapter, editor.getValue())
    });
});

function enableEdit() {
    $("#chapter-dropdown .editIcon").on("click", function (event) {
        $(this).parent().parent().siblings().removeClass("editing")
        $(this).parent().parent().siblings().children().find(".bi").attr("title", "Edit details")
        $(this).parent().parent().siblings().children().find(".bi").addClass("bi-pencil-square").removeClass("bi-floppy")
        $(this).parent().parent().siblings().children().find(".bi").parent().siblings().eq(0).prop('disabled', true)
        $(this).parent().parent().siblings().children().find(".bi").parent().siblings().eq(1).prop('disabled', true)
        $(this).parent().parent().siblings().children().find(".bi").parent().siblings().eq(0).hide()
        $(this).parent().parent().siblings().children().find(".bi").parent().siblings().eq(1).hide()
        $(this).parent().parent().siblings().children().find(".bi").parent().siblings().eq(2).show()
        $(this).parent().parent().siblings().children().find(".bi").parent().siblings().eq(3).show()
       
        if (!$(this).parent().parent().hasClass("editing")) {
            $(this).attr("title", "Save changes")
            $(this).parent().siblings().eq(0).val($(this).parent().siblings().eq(2).text())
            $(this).parent().siblings().eq(1).val($(this).parent().siblings().eq(3).text())
            $(this).parent().parent().addClass("editing")
            $(this).removeClass("bi-pencil-square").addClass("bi-floppy")
            $(this).parent().siblings().eq(0).prop('disabled', false)
            $(this).parent().siblings().eq(1).prop('disabled', false)
            $(this).parent().siblings().eq(0).show()
            $(this).parent().siblings().eq(1).show()
            $(this).parent().siblings().eq(2).hide()
            $(this).parent().siblings().eq(3).hide()
        } else {
            $(this).attr("title", "Edit details")
            $(this).parent().parent().removeClass("editing")
            $(this).addClass("bi-pencil-square").removeClass("bi-floppy")
            $(this).parent().siblings().eq(0).prop('disabled', true)
            $(this).parent().siblings().eq(1).prop('disabled', true)
            $(this).parent().siblings().eq(0).hide()
            $(this).parent().siblings().eq(1).hide()
            $(this).parent().siblings().eq(2).show()
            $(this).parent().siblings().eq(3).show()
            $(this).parent().siblings().eq(2).text($(this).parent().siblings().eq(0).val())
            $(this).parent().siblings().eq(3).text($(this).parent().siblings().eq(1).val())
            updateChapterDetails(parseInt($(this).parent().parent().attr("chapter")), $(this).parent().siblings().eq(2).text(), $(this).parent().siblings().eq(3).text())
        }
    });
}

function updateChapterDetails(chapter, name, description) {
    $.ajax({
        url: 'app/updateChapterDetails',
        method: 'POST',
        data: {
            chapter: chapter,
            name: name,
            description: description
        },
        success: function (response) {
            alertMessage("Saved details.")
            console.log("Saved details")
        },
        error: function (xhr, status, error) {
            alertMessage("Something Went Wrong!")
        }
    });
}


function enableDelete() {
    $("#chapter-dropdown .deleteIcon").on("click", function (event) {
        //deleteChapter(parseInt($(this).parent().parent().attr("chapter")))
        //$(this).parent().parent().remove()
        deleteChapterInt = parseInt($(this).parent().parent().attr("chapter"))
        nextChapterIdex = parseInt($(this).parent().parent().index())
        $("#deleteChapterName").text($(this).parent().siblings().eq(2).text())
        $("#removeModal").modal("show")
    });

    $("#deleteChapterButton").on("click", function (event) {
        localStorage.setItem("lastChapter", nextChapterIdex);
        document.location.href = `deleteChapter/${deleteChapterInt}`
    });

    $("#deleteChapterCloseButton").on("click", function (event) {
        $("#removeModal").modal("hide")
    });
}

function deleteChapter(chapter) {
    $.ajax({
        url: 'app/deleteChapter',
        method: 'POST',
        data: {
            chapter: chapter,
        },
        success: function (response) {
            alertMessage("Deleted chapter.")
            console.log("Deleted chapter")
        },
        error: function (xhr, status, error) {
            alertMessage("Something Went Wrong!")
        }
    });
}

function saveChapter(chapter, code) {
    code = code.replaceAll("'", "''")
    $.ajax({
        url: '../app/saveContent',
        method: 'POST',
        data: {
            chapter: chapter,
            code: code,
        },
        success: function (response) {
            $("#saveIcon").removeClass("bi-cloud-minus").addClass("bi-cloud-check")
            alertMessage("Saved content.")
            console.log("Saved content")
        },
        error: function (xhr, status, error) {
            alertMessage("Something Went Wrong!")
        }
    });
}