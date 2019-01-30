$(function () {

	// CCL

	// 注意：我想这里不能够用$("#my-comment-stage"),因为这样拿到的是一个jQuery对象
	// 实测果然不能给jQuery对象
	//var CM = new CommentManager($("#my-comment-stage"));

	var CM = new CommentManager(document.getElementById("my-comment-stage"));
	CM.init(); // 初始化
	CM.start(); // 弹幕开始播放

	// 建立播放时间轴
	var startTime = 0, iVal = -1;
	$("#btnTimer").click(function () {
		startTime = Date.now();// 设定起始时间
		if (iVal >= 0) {
			clearInterval(iVal); // 如果之前有定时器，就把它停掉
			// iVal清掉后，iVal的值是数字，清一次，数字增加一次
			//console.log(iVal);
			//console.log(iVal >= 0);
		}
		// 建立新的定时器
		iVal = setInterval(function () {
			var playTime = Date.now() - startTime; // 用起始时间和现在时间的差模拟播放时间
			CM.time(playTime); // 通报播放时间
			$("#playTimePos").text(playTime); // 显示播放时间 ms
		}, 100); // 模拟播放器每 100ms 通知播放时间
	});

	// 点击发送弹幕
	$("#btnInsert").click(function () {
		// 目前找不到方法，将字符串中的16进制数 转换为 数字型 16进制数
		// 当发现可将16进制转换为10进制数，可用
		// 因为字符串前0x，所以parseInt按16进制解析，返回与其等价的 十进制数
		console.log("0x" + $("#color-danmu").val().substr(1, 6));
		console.log(parseInt("0x" + $("#color-danmu").val().substr(1, 6)));

		var danmu = {
			"mode": parseInt($("#mode-danmu").val()),
			"text": $("#text-danmu").val(),
			// 注意：需将开始播放时间延迟一点，不然此次不会显示，当播放时间>stime时，弹幕不会被展示
			"stime": parseInt($("#playTimePos").text()) + 2000,
			// 官方demo，居然直接*1000, 延迟了这么多,实测根本没看到啊
			//"stime": parseInt($("#playTimePos").text()) * 1000 - 1,
			"size": 40,
			"color": parseInt("0x" + $("#color-danmu").val().substr(1, 6)),
			"border": true
		};
		// 将弹幕放入时间轴
		CM.insert(danmu);

		sendDanmuToServer(danmu);
	});

	$("#btnSend").click(function () {
		var danmu = {
			"mode": parseInt($("#mode-danmu").val()),
			"text": $("#text-danmu").val(),
			"stime": parseInt($("#playTimePos").text()) + 2000,
			"size": 30,
			"color": parseInt("0x" + $("#color-danmu").val().substr(1, 6)),
			"border": true
		};
		// 立刻显示一次这条弹幕
		CM.send(danmu);

		sendDanmuToServer(danmu);
	});

	// 开放 CM 对象到全局，这样就可以在 Console 控制端中操
	window.CM = CM;


	// SignalR

	$.connection.hub.start().done(function () {
		alert("连接DanmuHub成功");
	}).fail(function () {
		alert("连接DanmuHub失败");
	});

	var danmuHub = $.connection.danmuHub;

	// 供服务端调用的客户端js方法
	$.extend(danmuHub.client, {

		publicDanmu: function (danmu) {
			CM.insert(danmu);
			//CM.send(danmu);
		},

		tipMsg: function (message) {
			console.log(message);
		}
	});

	function sendDanmuToServer(danmu) {
		// 方法1：将json弹幕对象 转换为 json弹幕字符串 发送给服务端
		//danmuHub.server.sendDanmu(JSON.stringify(danmu));

		// 方法2：直接将json弹幕对象 发送给服务端
		danmuHub.server.sendDanmu(danmu);
	}
});