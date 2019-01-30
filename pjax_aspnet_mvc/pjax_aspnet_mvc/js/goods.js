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

// 应对非pjax请求，当页面加载完后，根据当前url 为 对应导航栏添加上active
$(function () {
	// eg: /Goods/Cat
	var pathName = window.location.pathname;
	$('.nav-list-item a').each(function () {
		if ($(this).attr('href').toLowerCase() == pathName.toLowerCase()) {
			$(this).parent('.nav-list-item').addClass('active');
		}
	});
});