var editor = CodeMirror.fromTextArea(document.getElementById('editor'), {
    theme: "ayu-dark",
    mode: "htmlmixed",
    lineNumbers: true,
    lineWrapping: false,
});
editor.save()