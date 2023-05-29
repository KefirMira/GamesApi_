import { Route, Routes } from "react-router-dom";
import ProtectedRoutes from "./ProtectedRoutes";
import AuthPage from "./components/AuthPage";
import GameList from "./components/GameList";

const Views = () => {
    return (
        <Routes>
            <Route path="/" element={<
                AuthPage />} />
            <Route element={<ProtectedRoutes />}>
                <Route path="/main" element={<GameList />} />
            </Route>
        </Routes>
    );
};

export default Views;