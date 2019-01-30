// 方式一
/*
$(document).pjax('[data-pjax] a, a[data-pjax]', '#pjax-container', {
	type: 'GET', // 默认
	fragment: '#content' 
	// 指定只要 #content 元素内的内容, 将其放入 #pjax-container 元素内, 不过注意：单这样做，其实没有实现局部请求，因为请求量为整个页面，只是只取了其中的 #content, 当然也可以让Net, Php等页面本身只具有局部html,但那样直接访问/Home/Net时就和 iframe一样只能看到局部
	// 所以还需要服务端配合当发现是pjax请求时，只返回部分视图
});
$(document).on('pjax:click', function (event) {
	console.log(event.target);
	$(event.target).parent('li').addClass('active').siblings('li').removeClass('active');
});
*/

// 方式二
if ($.support.pjax) {
	// 若支持 pjax
	// 全局设置
	$.pjax.defaults.type = 'POST';
	$.pjax.defaults.fragment = '#content';

	$(document).on('click', '[data-pjax] a, a[data-pjax]', function (event) {
		$.pjax.click(event, { container: '#pjax-container' });
		console.log(this);
		$(this).parent('li').addClass('active').siblings('li').removeClass('active');
	});
}




// common
$(document).on('pjax:send', function () {
	$('#loading').show();
});
$(document).on('pjax:complete', function () {
	$('#loading').hide("slow");
});