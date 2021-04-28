import React, { useContext, useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { BaseService } from "../../../services/BaseService";
import { AppContext } from "../../../context/AppContext";
import { IAppUserCustomDTO } from "../../../domain/custom/IAppUserCustomDTO";
import { IAppUserDTO } from "../../../domain/identity/IAppUserDTO";
import { searchUsersRequest } from "../../../services/custom/searchUser";
import { IUserFriendDTO } from "../../../domain/IUserFriendDTO";



const FriendsView = () => {
    const history = useHistory();
    const appContext = useContext(AppContext);
    const [user, setUser] = useState({ sentRequests: [] as IUserFriendDTO[], receivedRequests: [] as IUserFriendDTO[] } as IAppUserCustomDTO);
    const [search, setSearch] = useState("");
    const [searchResult, setSearchResult] = useState([] as IAppUserDTO[]);
    const [added, setAdded] = useState([] as IAppUserDTO[]);



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

    function editSearch(e: React.ChangeEvent<HTMLInputElement>) {
        setSearch(e.target.value)
    }

    async function searchUsers() {
        await searchUsersRequest<IAppUserDTO>(search, appContext.data.token)
            .then(data => {
                console.log(data)
                setSearchResult(data!);
            });
    }

    function addButton(user: IAppUserDTO) {
        if (added.find(a => a.id === user.id)) {
            return (
                <div className="float-right">Taotlus saadeti</div>
            )
        }
        return (
            <button type="button" className="btn btn-success btn-sm float-right" onClick={e => addUser(user)}>Lisa sõbraks</button>
        )
    }

    const addUser = async (user: IAppUserDTO) => {
        var entity = { recipientId: user.id, appUserId: appContext.data.appUserId, pending: true, accepted: false } as IUserFriendDTO;
        await BaseService
            .createEntity<IUserFriendDTO>(entity, "userFriends", appContext.data.token)
            .then(data => {
                console.log(data);
            });

        setAdded([...added, user])
    }

    const editrequest = async (uf: IUserFriendDTO, addFriend: boolean) => {
        console.log(uf)
        var entity = { id: uf.id, recipientId: uf.recipientId, appUserId: uf.appUserId, pending: false, accepted: false } as IUserFriendDTO;
        if (addFriend) {
            entity.accepted = true;
        }
        await BaseService
            .updateEntity<IUserFriendDTO>(entity, "userFriends", appContext.data.token)
            .then(d => {
                data();
            });

    }

    function recievedBlock() {
        if (user.receivedRequests) {
            return (<>
                {user.receivedRequests.filter(a => a.pending).map((request, i) => (
                    <>
                        <div>
                            Taotlus {request.appUser!.firstName} {request.appUser!.firstName} poolt.
                            <button type="button" className="btn btn-danger btn-sm float-right" onClick={e => editrequest(request, false)}>Keeldu</button>
                            <button type="button" className="btn btn-success btn-sm float-right" onClick={e => editrequest(request, true)}>Lisa vastu</button>
                        </div>

                    </>
                ))}
            </>)
        }
        return <></>
    }

    function friendsBlock() {
        var arr = [] as IUserFriendDTO[];
        if (user.sentRequests) {
            arr = arr.concat(user.sentRequests)
        }
        if (user.receivedRequests) {
            arr = arr.concat(user.receivedRequests)
        }


        return (<>
            {arr.map((request, i) => (
                <>
                    {friendsBlockFriend(request)}
                </>
            ))}
        </>)
    }

    function friendsBlockFriend(uf: IUserFriendDTO) {
        //console.log(uf)
        if (uf.accepted) {
            if (uf.appUserId === appContext.data.appUserId) {
                return (<>
                    <div>
                        {uf.recipient!.firstName} {uf.recipient!.firstName}
                    </div>
                </>)
            }
            return (<>
                <div>
                    {uf.appUser!.firstName} {uf.appUser!.firstName}
                </div>

            </>)
        }
        return <></>
    }



    return (
        <div className="container">
            <button type="button" className="btn button-bg-purple-dark" data-toggle="modal" data-target="#exampleModal">
                Lisa uus sõber
            </button>
            <p></p>

            <div className="accordion" id="accordionExample">
                <div className="card skippy border-white">
                    <div className="card-header" id="headingOne">
                        <h2 className="mb-0">
                            <button className="btn btn-link btn-block text-left" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                <div className="text-white">Sõbrataotlused</div>
                            </button>
                        </h2>
                    </div>

                    <div id="collapseOne" className="collapse skippy-light collapsed" aria-labelledby="headingOne" data-parent="#accordionExample">
                        <div className="card-body">
                            {recievedBlock()}
                        </div>
                    </div>
                </div>
                <div className="card skippy border-white">
                    <div className="card-header" id="headingTwo">
                        <h2 className="mb-0">
                            <button className="btn btn-link btn-block text-left show" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                <div className="text-white">Minu Sõbrad</div>
                            </button>
                        </h2>
                    </div>
                    <div id="collapseTwo" className="collapse skippy-light" aria-labelledby="headingTwo" data-parent="#accordionExample">
                        <div className="card-body">
                            {friendsBlock()}
                        </div>
                    </div>
                </div>
            </div>

            <div className="modal fade" id="exampleModal" tabIndex={-1} aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title" id="exampleModalLabel">Lisa uus sõber</h5>
                            <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div className="modal-body">
                            <div className="mb-3">
                                <label className="form-label">Otsi sõpra:</label>
                                <input value={search} type="text" name="search" className="form-control" onChange={e => editSearch(e)} />
                                <p></p>
                                <button type="button" className="btn btn-primary" onClick={e => searchUsers()}>Otsi</button>
                                <p></p>
                            </div>
                            {searchResult.map((result, i) => (
                                <div className="mb-3">
                                    {result.firstName} {result.lastName}
                                    {addButton(result)}
                                </div>
                            ))}

                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-secondary" data-dismiss="modal">Sulge</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default FriendsView;
