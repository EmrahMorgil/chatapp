import React from "react";

interface IProtected {
    loggedIn: boolean,
    children: any,
}

const Protected: React.FC<IProtected> = ({ loggedIn, children }) => {

    const redirect = () => {
        window.location.href = `${process.env.REACT_APP_BASE_URL + "/login"}`;
    }

    if (!loggedIn) {
        return redirect();
    }
    return children;
};

export default Protected;