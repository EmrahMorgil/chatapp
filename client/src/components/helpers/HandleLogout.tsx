export const HandleLogout = () => {
    localStorage.removeItem("activeUser");
    localStorage.removeItem("takerUser");
    localStorage.removeItem("token");
    window.location.href = `${process.env.REACT_APP_BASE_URL + "/login"}`;
}