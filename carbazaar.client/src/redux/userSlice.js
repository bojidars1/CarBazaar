const { createSlice } = require("@reduxjs/toolkit")

const initalState = {
    user: null
}

const userSlice = createSlice({
    name: 'user',
    initalState,
    reducers: {
        setUser: (state, action) => {
            state.user = action.payload;
        },
        clearUser: (state) => {
            state.user = null;
        }
    }
});

export const { setUser, clearUser } = userSlice.actions;

export default userSlice.reducer;