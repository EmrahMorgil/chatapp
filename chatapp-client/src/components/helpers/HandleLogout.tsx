export const HandleLogout = () => {
    localStorage.removeItem("activeUser");
    localStorage.removeItem("takerUser");
    localStorage.removeItem("token");
    window.location.href = `${window.location.href + "login"}`;
}