import React, { useContext } from "react";
import { Link, useHistory } from "react-router-dom";
import { AppContext } from "../../context/AppContext";
import jwt_decode from "jwt-decode";
import { IJwtResponseDTO } from "../../domain/identity/IJwtResponseDTO";


const Footer = () => {
    const appContext = useContext(AppContext);
    const history = useHistory();


    return (
        <footer>
            <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                <div className="container">
                    <a className="navbar-brand" href="/">Footer </a>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    
                </div>
            </nav>
        </footer>
    );
}

export default Footer;