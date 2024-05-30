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