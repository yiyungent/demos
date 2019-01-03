var danmakuHub;

$(function () {
	// SignalR
	$.connection.hub.url = "http://api.moeci.com/signalr/";
	$.connection.hub.start().done(function () {
		alert("连接弹幕服务器成功");
	}).fail(function () { alert("连接弹幕服务器失败"); });

	danmakuHub = $.connection.danmakuHub;

	$.extend(danmakuHub.client, {
		sendDanmaku: function (danmakuObj) {
			console.log(111);
			if (danmakuObj.hasOwnProperty('stime')) {
				// 弹幕有时间轴位置，那就插入时间轴
				CM.insert(danmakuObj);
			} else {
				// 弹幕没有时间轴位置，就立刻显示（不记录）
				// 不记录在时间轴中，也就无法通过改变播放时间位置看到此条弹幕
				CM.send(danmakuObj);
			}
		},
		sendTipMsg: function (message) {
			console.log(message);
		}
	});

	// CCL
	var CM = new CommentManager(document.getElementById("my-comment-stage"));
	CM.init(); // 初始化
	CM.start(); // 弹幕开始播放

	// 点击开始模拟时间轴开始播放
	var startTime = 0, iVal = -1;
	$("#btnTimer").click(function () {
		startTime = Date.now();
		if (iVal >= 0) {
			clearInterval(iVal);
		}
		iVal = setInterval(function () {
			var playTime = Date.now() - startTime;
			CM.time(playTime);
			$("#playTimePos").text(playTime);
		}, 100);
	});

	// 点击将弹幕插入时间轴
	$("#btnInsert").click(function () {
		var danmaku = {
			"mode": parseInt($("#mode-danmaku").val()),
			"text": $("#text-danmaku").val(),
			"stime": CM._lastPosition + 200,
			"size": 40,
			"color": parseInt("0x" + $("#color-danmaku").val().substr(1, 6)),
			"border": true
		};
		// 方法一：发送弹幕到服务端（可由服务端进行弹幕内容过滤），由服务端再发送到所有客户端（包括当前用户），不过这个方法有个缺点，由于服务端不记录"border"，所以自己发的弹幕经过服务端下发后，自己也看不到弹幕border，当然也可以在服务端判断，每一个弹幕消息都加用户标识，发送给客户端时给当前用户标识的弹幕加border并加border的条只发给当前用户，其它发无border的

		// 其实也有第2种方法，即这里客户端也调取CM.insert()，不过服务端发送时，发送给除当前用户的所有用户，这样就不会出现发送一条弹幕，显示2条弹幕了
		sendDanmakuToServer(danmaku);
	});

	// 点击发送弹幕（即时有效性弹幕，不记录在时间轴）
	$("#btnSend").click(function () {
		var danmaku = {
			"mode": parseInt($("#mode-danmaku").val()),
			"text": $("#text-danmaku").val(),
			"size": 40,
			"color": parseInt("0x" + $("#color-danmaku").val().substr(1, 6)),
			"border": true
		};

		sendDanmakuToServer(danmaku);
	});

	window.CM = CM;
});

// 发送弹幕到服务端
function sendDanmakuToServer(danmakuObj) {
	danmakuHub.server.sendDanmaku(danmakuObj);
}