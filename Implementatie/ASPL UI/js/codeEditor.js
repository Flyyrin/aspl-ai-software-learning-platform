var editor = CodeMirror.fromTextArea(document.getElementById('editor'), {
    theme: "ayu-dark",
    mode: "python",
    lineNumbers: true,
    lineWrapping: false,
});
editor.save()