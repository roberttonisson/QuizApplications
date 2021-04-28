import React, { useState } from "react";
import Header from "./components/shared/Header";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Home from "./components/Home";
import { AppContextProvider, AppContextInitialState, IAppContext } from "./context/AppContext";
import Login from "./components/account/Login";
import Register from "./components/account/Register";
import CreateQuiz from "./components/views/Quiz/CreateQuiz";
import { IJwtResponseDTO } from "./domain/identity/IJwtResponseDTO";
import EditQuiz from "./components/views/Quiz/EditQuiz";
import FinishedQuizView from "./components/views/Quiz/FinishedQuizView";
import MyQuizzes from "./components/views/Quiz/MyQuizzes";
import UpcomingQuizView from "./components/views/Quiz/UpcomingQuizView";
import FriendsView from "./components/views/Friends/FriendsView";
import FriendQuizzes from "./components/views/Quiz/FriendQuizzes";
import QuizGame from "./components/views/Quiz/QuizGame";
import Footer from "./components/shared/Footer";

const App = () => {
    const setData = (data: IJwtResponseDTO) => {
        setAppState({...appState, data: data});
    }


    const initialAppState = {
        ...AppContextInitialState,
        setData
    } as IAppContext;
    const [appState, setAppState] = useState(initialAppState);

    return (
    <AppContextProvider value={appState}>
        <Router>
            <Header />
            <Switch>
                <Route exact path="/">
                    <Home />
                </Route>
                <Route path="/home">
                    <Home />
                </Route>
                <Route path="/login">
                    <Login />
                </Route>
                <Route path="/register">
                    <Register />
                </Route>
                <Route path="/friends"> 
                    <FriendsView />
                </Route>
                <Route path="/createQuiz"> 
                    <CreateQuiz />
                </Route>
                <Route path="/myquizzes">
                    <MyQuizzes />
                </Route>
                <Route path="/friendquizzes">
                    <FriendQuizzes />
                </Route>
                <Route path="/finishedquiz/:id">
                    <FinishedQuizView />
                </Route>
                <Route path="/upcomingquiz/:id">
                    <UpcomingQuizView />
                </Route>
                <Route path="/editquiz/:id">
                    <EditQuiz />
                </Route>
                <Route path="/quizgame/:id">
                    <QuizGame />
                </Route>
                <h1>Page not found 404</h1>
            </Switch>
        </Router>
    </AppContextProvider>
    );
};

export default App;