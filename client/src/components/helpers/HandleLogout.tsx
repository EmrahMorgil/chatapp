import CookieManager from "./CookieManager";

export const HandleLogout = () => {
    CookieManager.clearCookies();
    sessionStorage.removeItem("takerUser");
    sessionStorage.removeItem("room");
    window.location.href = window.location.origin + "/login";
}