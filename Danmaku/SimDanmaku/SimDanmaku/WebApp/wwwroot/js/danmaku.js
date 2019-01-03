var danmakuBaseUrl = "http://localhost:16204";
// start 搭建结构
buildHTMLStruct();
function buildHTMLStruct() {

	var abpDiv = document.createElement("div");
	abpDiv.setAttribute("class", "abp");
	abpDiv.style.height = document.body.offsetHeight + "px";
	var containerDiv = document.createElement("div");
	containerDiv.setAttribute("class", "container");
	containerDiv.setAttribute("id", "danmaku-stage");
	containerDiv.innerHTML = document.body.innerHTML;
	abpDiv.appendChild(containerDiv);
	document.body.innerHTML = "";
	document.body.appendChild(abpDiv);

	var danmakuBox = document.createElement("div");
	danmakuBox.setAttribute("class", "danmakuBox");
	var danmakuInput = document.createElement("input");
	danmakuInput.setAttribute("type", "text");
	danmakuBox.appendChild(danmakuInput);
	var btnAddDanmaku = document.createElement("button");
	btnAddDanmaku.setAttribute("id", "btnAddDanmaku");
	btnAddDanmaku.innerHTML = "发射弹幕";
	danmakuBox.appendChild(btnAddDanmaku);
	document.body.appendChild(danmakuBox);

	var cclCss = document.createElement("link");
	cclCss.setAttribute("rel", "stylesheet");
	cclCss.setAttribute("href", danmakuBaseUrl + "/lib/ccl/css/CommentCoreLibrary.css");
	document.body.appendChild(cclCss);
	var cclJs = document.createElement("script");
	cclJs.setAttribute("src", danmakuBaseUrl + "/lib/ccl/js/CommentCoreLibrary.js");
	document.body.appendChild(cclJs);
	var signalRJs = document.createElement("script");
	signalRJs.setAttribute("src", danmakuBaseUrl + "/lib/signalr/signalr.js");
	document.body.appendChild(signalRJs);
}
// end 搭建结构

function init() {
	// start CCL
	var danmakuManager = new CommentManager(document.getElementById("danmaku-stage"));
	danmakuManager.init();
	danmakuManager.start();
	window.danmakuManager = danmakuManager;
	// end CCL

	// start signalR
	var connection = new signalR.HubConnectionBuilder().withUrl(danmakuBaseUrl + "/danmakuHub").build();

	connection.start().catch(function (err) {
		return console.error(err.toString());
	});

	connection.on("ReceiveDanmaku", tipMsg);

	function tipMsg(msg) {
		console.log(msg);
	}

	document.getElementById("btnAddDanmaku").onclick = function () {
		var danmakuJsonObj = JSON.parse(document.getElementById("danmakuStr").value);
		connection.invoke("SendDanmaku", danmakuJsonObj);
	}

	window.connection = connection;
	// end signalR
}
setTimeout(init, 2000);