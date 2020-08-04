function openIframe() {
    document.getElementById("content_iframe").src = "tiantian.html";
}

//事件绑定
document.getElementById("content_menu_item_tiantian").addEventListener("click", openIframe);

