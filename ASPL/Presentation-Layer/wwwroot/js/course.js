var course = 1;
$(document).ready(function () {
    $("#course-dropdown a").on("click", function (event) {
        var newCourse = parseInt($(this).attr("course"));
        if (newCourse != course) {
            $(this).parent().find(".bi").removeClass("bi-check-circle").addClass("bi-circle")
            $(this).find(".bi").removeClass("bi-circle").addClass("bi-check-circle")
            course = newCourse
            loadChapters()
        }
    });
});

function loadChapters() {
    $.ajax({
        url: 'app/getChapters/',
        data: { course: course },
        type: 'GET',
        success: function (response) {
            alertMessage("Loading Course")

            ----
            $.each(chapters, function (index, chapter) {
               var li = ""

        
                $('#list-container').append($link);
            });
            -----
        },
        error: function (xhr, status, error) {
            alertMessage("Something Went Wrong!")
        }
    });
}