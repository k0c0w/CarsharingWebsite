export function fixHeader() {
    const header = document.getElementsByTagName("header")[0];

    if(header && !header.classList.contains("fixed")) {
        header.classList.add("fixed");
    }
}
