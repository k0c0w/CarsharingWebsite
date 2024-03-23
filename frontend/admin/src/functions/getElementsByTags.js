export function getElementsByTagNames(list,obj) {
	if (!obj) obj = document;
	let tagNames = list.split(',');
	let resultArray = new Array();
	for (let i=0;i<tagNames.length;i++) {
		let tags = obj.getElementsByTagName(tagNames[i]);
		for (let j=0;j<tags.length;j++) {
			resultArray.push(tags[j]);
		}
	}
	let testNode = resultArray[0];
	if (!testNode) return [];
	if (testNode.sourceIndex) {
		resultArray.sort(function (a,b) {
				return a.sourceIndex - b.sourceIndex;
		});
	}
	else if (testNode.compareDocumentPosition) {
		resultArray.sort(function (a,b) {
				return 3 - (a.compareDocumentPosition(b) & 6);
		});
	}
	return resultArray;
}