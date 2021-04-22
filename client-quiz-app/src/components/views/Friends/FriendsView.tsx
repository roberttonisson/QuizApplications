import React, { useContext, useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { BaseService } from "../../../services/BaseService";
import { AppContext } from "../../../context/AppContext";
import { IAppUserCustomDTO } from "../../../domain/custom/IAppUserCustomDTO";
import { IAppUserDTO } from "../../../domain/identity/IAppUserDTO";



const FriendsView = () => {
    const history = useHistory();
    const appContext = useContext(AppContext);
    const [user, setUser] = useState({} as IAppUserCustomDTO);
    const [search, setSearch] = useState("");
    const [searchResult, setSearchResult] = useState([] as IAppUserDTO[]);



    const data = async () => {
        await BaseService
            .getSingle<IAppUserCustomDTO>('userFriends/myFriends', appContext.data.token)
            .then(data => {
                console.log(data)
                setUser(data!);
            });
    }

    useEffect(() => {
        data();
    }, []);

    function editSearch(e: React.ChangeEvent<HTMLInputElement>){
        setSearch(e.target.value)
    }

    async function searchUsers(){
        await searchUsersRequest<IAppUserDTO>()
        .then(data => {
            console.log(data)
            setSearchResult(data!);
        });
    }

    async function searchUsersRequest<T>(): Promise<T[] | null> {
        try {
            const response = await BaseService.axios
                .post("userFriends/search", {search: search}, {
                    headers: {
                        Authorization: "Bearer " + appContext.data.token
                    }
                })

            if (response.status >= 200 && response.status < 300) {
                return response.data;
            }

            return null;
        }
        catch (reason) {
            return reason.status;
        }
    }




    return (

        <div className="container border rounded">
            <button type="button" className="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
                Add a new friend
            </button>

            <div className="modal fade" id="exampleModal" tabIndex={-1} aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title" id="exampleModalLabel">Find a friend</h5>
                            <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div className="modal-body">
                            <div className="mb-3">
                                <label className="form-label">Search for a friend:</label>
                                <input value={search} type="text" name="search" className="form-control" onChange={e => editSearch(e)} />
                                <p></p>
                                <button type="button" className="btn btn-primary" onClick={e => searchUsers()}>Search</button>
                                <p></p>
                            </div>

                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default FriendsView;
