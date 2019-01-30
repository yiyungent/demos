<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Simple.aspx.cs" Inherits="Simple" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <%-- 不对啊，难道"~"是MVC里的，发现用"~"并没有自动解析为网站根目录，而是就是"~"，从而导致了路径错误 --%>
    <script src="~/Scripts/jquery-1.6.4.js"></script>
    <script src="~/Scripts/jquery.signalR-2.3.0.js"></script>
    <%-- 虚拟路径，此文件由signalr动态生成 --%>
    <script src="~/signalr/hubs"></script>
    <script type="text/javascript">
        $(function () {
            var ticker = $.connection.simpleHub;
            $.connection.hub.start();

            // 客户端=>服务端
            $("#btn").click(function () {
                // 链接完成以后，可以发送消息至服务端
                ticker.server.sendMsg("需要发送的消息");
            });

            // 服务端=>客户端
            ticker.client.msg = function (data) {
                console.log(data);
            }
        });
    </script>
</head>
<body>
    
</body>
</html>
