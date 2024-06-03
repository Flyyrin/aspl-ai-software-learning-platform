var allowedElements = {
    text: '<p>{{content}}</p>',
    title: '<h1 class="mt- 0">{{content}}</h1>',
    subtitle: '<h2 class="mt- 0">{{content}}</h2>',
    header: '<h3 class="mt- 0">{{content}}</h3>',
    snippet: '<pre><code class="language-python my-3">{{content}}</code></pre>',
}
function decodeCode(code) {
    const segments = code.match(/<[^>]+>.*?<\/[^>]+>/gs);

    var newCode = ""
    $.each(segments, function (index, segment) {
        const tagName = segment.match(/<([^>]+)>/)[1];
        var tagContent = segment.match(/<[^>]+>(.*?)<\/[^>]+>/s)[1];
        if (tagName in allowedElements) {
            var format = allowedElements[tagName]

            tagContent = tagContent.replaceAll('((', "<code>")
            tagContent = tagContent.replaceAll('))', "</code>")
            newCode += format.replace("{{content}}", tagContent)
        }
    });

    return newCode
}