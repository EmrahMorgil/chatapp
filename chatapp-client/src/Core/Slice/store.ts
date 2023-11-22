import { configureStore } from "@reduxjs/toolkit";

import usersSlice from "./users/usersSlice";
import messagesSlice from "./messages/messagesSlice";


export const store = configureStore({
    reducer: {
        messages: messagesSlice,
        users: usersSlice,
    },
});

//-----------------------------------------
export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch