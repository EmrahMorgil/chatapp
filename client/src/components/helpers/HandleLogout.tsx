import CookieManager from "./CookieManager";

export const HandleLogout = () => {
    CookieManager.clearCookies();
    sessionStorage.removeItem("takerUser");
    sessionStorage.removeItem("room");
    window.location.href = `${process.env.REACT_APP_BASE_URL + "/login"}`;
}