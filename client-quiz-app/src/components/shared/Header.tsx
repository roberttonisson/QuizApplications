import React, { useContext } from "react";
import { Link, useHistory } from "react-router-dom";
import { AppContext } from "../../context/AppContext";
import jwt_decode from "jwt-decode";
import { IJwtResponseDTO } from "../../domain/identity/IJwtResponseDTO";


const Header = () => {
    const appContext = useContext(AppContext);
    const history = useHistory();

    function logOut(e: React.MouseEvent<HTMLButtonElement, MouseEvent>) {
        appContext.setData({} as IJwtResponseDTO)
        history.push('/');
        e.preventDefault();
    }

    function isLoggedIn(loggedIn: Boolean) {
        if (loggedIn) {
            return (
                <ul className="navbar-nav">
                    <li className="nav-item" style={{ paddingRight: "10px" , color: '#ac92d4'}}>
                        {jwt_decode<any>(appContext.data!.token!)['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']}
                    </li>
                    <li className="nav-item">
                        <button className="btn button-bg-purple-light" onClick={(e) => logOut(e)}>Logout</button>
                    </li>
                </ul>
            );
        }

        return (
            <ul className="navbar-nav">,.
                <li className="nav-item btn button-bg-purple-light">
                    <Link to="/login" className="text-white">Login </Link>
                </li>
                &nbsp;&nbsp;
                <li className="nav-item btn button-bg-purple-light" style={{ paddingLeft: "10px" }}>
                    <Link to="/register" className="text-white button-bg-purple-light">Register </Link>
                </li>
            </ul>
        );
    }
    function home(loggedIn: Boolean) {
        if (loggedIn) {
            return (
                <>
                    <li className="nav-item" style={{ paddingRight: "10px" }}>
                        <Link to="/" className="text-white">Home</Link>
                    </li>
                    <li className="nav-item" style={{ paddingRight: "10px" }}>
                        <Link to="/createQuiz" className="text-white">Create QUIZ</Link>
                    </li>
                    <li className="nav-item" style={{ paddingRight: "10px" }}>
                        <Link to="/myQuizzes" className="text-white">My quizzes</Link>
                    </li>
                    <li className="nav-item" style={{ paddingRight: "10px" }}>
                        <Link to="/friends" className="text-white">Friends</Link>
                    </li>
                    <li className="nav-item" style={{ paddingRight: "10px" }}>
                        <Link to="/friendquizzes" className="text-white">Quizzes </Link>
                    </li>

                </>
            );
        }

        return (
            <li className="nav-item" style={{ paddingRight: "10px" }}>
                <Link to="/" className="text-white">Home</Link>
            </li>
        );
    }

    function roleChoice() {
        if (appContext.data.token == null) {
            return (<></>);
        }
        return (<></>);
    }

    return (
        <header>
            <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light border-bottom box-shadow mb-3 skippy">
                <div className="container">
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
                        <ul className="navbar-nav">
                            {isLoggedIn(appContext.data.token != null && appContext.data.token.length > 0)}
                        </ul>

                        <ul className="navbar-nav flex-grow-1">
                            {home(appContext.data.token != null && appContext.data.token.length > 0)}
                            <li className="nav-item" style={{ paddingRight: "10px" }}>
                                {roleChoice()}
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
    );
}

export default Header;