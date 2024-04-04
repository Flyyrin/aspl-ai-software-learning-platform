var courseHeight;
var outputHeight;
var lastWidth;

$(document).ready(function(){
    resizeCourse()
    resizeCode()
    resizeOutput()
    courseHeight = $(".course").outerHeight()
    outputHeight = $(".output").outerHeight()
    lastWidth = $(window).width()
    $('#question-section-content').on('hide.bs.collapse', function () {
        $(".course").outerHeight(courseHeight, true)
    });
  
    $('#question-section-content').on('shown.bs.collapse', function () {
        resizeCourse();
    });

    $('#question-section-content').on('hidden.bs.collapse', function () {
        resizeCourse();
    });

    $('#explaination-section-content').on('hide.bs.collapse', function () {
        $(".output").outerHeight(outputHeight, true)
    });
  
    $('#explaination-section-content').on('shown.bs.collapse', function () {
        resizeOutput();
    });

    $('#explaination-section-content').on('hidden.bs.collapse', function () {
        resizeOutput();
    });

    $('.nav-pills .nav-item').click(function() { 
        var index = $(this).index();
        if (index == 0) {
            $(".course-section").show();
            $(".code-section").hide();
            $(".output-section").hide();
        } else if (index == 1) {
            $(".course-section").hide();
            $(".code-section").show();
            $(".output-section").hide();
            resizeCode()
        } else if (index == 2) {
            $(".course-section").hide();
            $(".code-section").hide();
            $(".output-section").show();
        }
    });
});

$(window).on("resize", function() {
    // if (Math.abs(lastWidth - $(window).width()) > 50) {
    //     window.top.location = window.top.location
    // }
    lastWidth = $(window).width()

    if ($(window).width() < 768) {
        $(".course-section").show();
        $(".code-section").hide();
        $(".output-section").hide();
        $('.nav-pills .nav-item .nav-link').each(function(i, item){    
            $(item).removeClass("active");
        });
        $('.nav-pills .nav-item .nav-link').first().addClass("active");
    }
    else {
        $(".course-section").show();
        $(".code-section").show();
        $(".output-section").show();
    }

    resizeCourse()
    resizeCode()
    resizeOutput()
});

function resizeCourse() {
    windowHeight = $(window).height()
    tabHeaderHeight = $(".tab-header").outerHeight();
    headerHeight = $(".header").outerHeight();
    footerHeight = $(".question-section").outerHeight();
    $(".course").outerHeight(windowHeight - tabHeaderHeight - headerHeight - footerHeight, true)
    $(".course-section").outerHeight(windowHeight - tabHeaderHeight, true)
}

function resizeCode() {
    windowHeight = $(window).height()
    tabHeaderHeight = $(".tab-header").outerHeight();
    footerHeight = $(".action-footer").outerHeight();
    console.log(footerHeight)
    $(".CodeMirror").outerHeight(windowHeight - tabHeaderHeight - footerHeight, true)
    $(".code-section").outerHeight(windowHeight - tabHeaderHeight, true)
}

function resizeOutput() {
    windowHeight = $(window).height()
    tabHeaderHeight = $(".tab-header").outerHeight();
    footerHeight = $(".explaination-section").outerHeight();
    $(".output").outerHeight(windowHeight - tabHeaderHeight - footerHeight, true)
    $(".output-section").outerHeight(windowHeight - tabHeaderHeight, true)
}