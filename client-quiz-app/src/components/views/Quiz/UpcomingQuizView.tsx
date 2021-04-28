import React, { useContext, useEffect, useState } from "react";
import { useHistory, useParams } from "react-router-dom";
import { BaseService } from "../../../services/BaseService";
import { AppContext } from "../../../context/AppContext";
import { IQuizDTO } from "../../../domain/IQuizDTO";
import MyQuizzes from "./MyQuizzes";
import { FormatDate } from "../../../base/FormatDate";
import { FormHelperText, InputLabel, MenuItem, Select, TextField } from "@material-ui/core";
import { ITeamDTO } from "../../../domain/ITeamDTO";
import { IAppUserDTO } from "../../../domain/identity/IAppUserDTO";
import { IUserFriendDTO } from "../../../domain/IUserFriendDTO";
import { IAppUserCustomDTO } from "../../../domain/custom/IAppUserCustomDTO";
import $ from 'jquery';
import { IAddTeamDTO } from "../../../domain/custom/IAddTeamDTO";
import { ITeamUserDTO } from "../../../domain/ITeamUserDTO";


const UpcomingQuizView = () => {
    let { id } = useParams<{ id: string }>();
    const history = useHistory();
    const appContext = useContext(AppContext);
    const [user, setUser] = useState({ sentRequests: [] as IUserFriendDTO[], receivedRequests: [] as IUserFriendDTO[] } as IAppUserCustomDTO);
    const [quiz, setQuiz] = useState({ teams: [] as ITeamDTO[] } as IQuizDTO);
    const [team, setTeam] = useState({ quizId: '', appUserId: appContext.data.appUserId, name: '' } as ITeamDTO);
    const [selectedUsers, setSelectedUsers] = useState([] as string[]);


    const data = async () => {
        await BaseService
            .getEntity<IQuizDTO>(id, 'quiz', appContext.data.token)
            .then(data => {
                setQuiz(data!);
                console.log(data)
            });
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

    const registerTeam = async () => {
        var entity = { team: { quizId: quiz.id, name: team.name, appUserId: appContext.data.appUserId } as ITeamDTO, members: selectedUsers } as IAddTeamDTO
        await BaseService.createEntity<IAddTeamDTO>(entity, 'teams/members', appContext.data!.token!)
            .then(d => {
                $('#exampleModal').modal('hide')
                data();
            })
    };

    function handleQuizInputChange(e: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) {
        const { name, value } = e.target;
        setTeam({ ...team, [name]: value });
        console.log(team.name)
    };

    function getTotalQuestions() {
        var ret = 0;
        if (quiz.quizTopics) {
            quiz.quizTopics!.forEach(qt => {
                if (qt.topicQuestions) {
                    ret += qt.topicQuestions!.length
                }

            });
        }
        return ret;
    }

    function getTotalQuestionBlocks() {
        if (quiz.quizTopics) {
            return quiz.quizTopics.length
        }
        return 0;
    }

    function getAllFriends() {
        var arr = [] as IUserFriendDTO[];
        if (user.sentRequests) {
            arr = arr.concat(user.sentRequests)
        }
        if (user.receivedRequests) {
            arr = arr.concat(user.receivedRequests)
        }
        return (
            <>
                {arr.map((uf, i) => (
                    <>{getAllFriendsHelper(uf)}</>
                ))}
            </>)
    }

    function getAllFriendsHelper(uf: IUserFriendDTO) {
        //console.log(uf)
        if (uf.accepted) {
            if (uf.appUserId === appContext.data.appUserId) {
                return (
                    <option value={uf.recipientId}>{uf.recipient!.firstName} {uf.recipient!.firstName}</option>
                )
            }
            return (
                <option value={uf.appUserId}>{uf.appUser!.firstName} {uf.appUser!.firstName}</option>
            )
        }
        return
    }
    function handleInputList(e: React.ChangeEvent<HTMLSelectElement>) {

        var values = $('#exampleFormControlSelect2').val() as string[];
        setSelectedUsers(values);
        console.log(selectedUsers)

    }

    function playButton() {
        if(quiz.appUserId === appContext.data.appUserId){
            return (
                <>
                <button type="button" className="btn button-bg-purple-dark float-right" onClick={e => play()}>ALUSTA</button>
                <button type="button" className="btn button-bg-purple-dark float-right" onClick={e => edit()}>Muuda</button>
                </>
            )
        }
        return
    }

    function play() {
        history.push('/quizgame/' + quiz.id)
    }
    function edit() {
        history.push('/editquiz/' + quiz.id)
    }

    return (
        <div className="container">
            <div className="jumbotron jumbo">
                <h1 className="display-4">{quiz.title}</h1>
                <p className="lead">This is a simple hero unit, a simple jumbotron-style component for calling extra attention to featured content or information.</p>
                <hr className="my-4"></hr>
                <p>Küsimuseplokke: {getTotalQuestionBlocks()}</p>
                <p>Küsimusi kokku: {getTotalQuestions()}</p>
                <p>Toimumise aeg: {FormatDate(quiz.start)}</p>
                <a className="btn button-bg-purple-dark" role="button" data-toggle="collapse" href="#multiCollapseExample1" aria-expanded="false" aria-controls="multiCollapseExample1">Tiimid</a>
                <button type="button" className="btn button-bg-purple-light btn-lg float-right" data-toggle="modal" data-target="#exampleModal">Registreeri tiim</button>
                {playButton()}
                <div className="modal fade" id="exampleModal" tabIndex={-1} aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div className="modal-dialog">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title" id="exampleModalLabel">Registreeri tiim</h5>
                                <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div className="modal-body">
                                <form>
                                    <TextField
                                        id="standard-basic"
                                        label="Tiiminimi"
                                        name="name"
                                        placeholder="sisestage nimi"
                                        type="text"
                                        value={team.name}
                                        onChange={e => handleQuizInputChange(e)} />
                                    <div className="form-group">
                                        <label htmlFor="exampleFormControlSelect2">Example multiple select</label>
                                        <select multiple className="form-control" id="exampleFormControlSelect2" onChange={e => handleInputList(e)}>
                                            {getAllFriends()}
                                        </select>
                                    </div>
                                </form>
                            </div>
                            <div className="modal-footer">
                                <button type="button" className="btn btn-secondary" data-dismiss="modal">Sulge</button>
                                <button type="button" className="btn button-bg-purple-dark" onClick={e => registerTeam()}>Registreeri tiim</button>
                            </div>
                            
                        </div>
                    </div>
                </div>
                <p></p>
                <div className="collapse multi-collapse" id="multiCollapseExample1">
                    {quiz.teams!.map((team, i) => (
                        <div className="card card-body d-flex justify-content-center jumbo-bottom" style={{flexDirection: "initial"}}>
                            <div className="card jumbo-bottom bg-white" style={{ width: '30rem' }}>
                                <div className="card-body">
                                    <div>
                                    <h5 className="card-title">{team.name}</h5>
                                    <h6 className="card-subtitle mb-2 text-muted">Kapten: {team.appUser!.firstName} {team.appUser!.lastName}</h6>
                                    {team.teamUsers!.map((teamUser, i) => (
                                        <>{teamMembers(teamUser, team)}</>
                                    ))}
                                    </div>
                                </div>
                            </div>
                        </div>
                    ))}

                </div>
            </div>
        </div>
    )
    function teamMembers(teamUser: ITeamUserDTO, team: ITeamDTO) {
        if (teamUser.appUserId === team.appUserId) {
            return
        }
        return (
            <div className="card-text">{teamUser.appUser!.firstName} {teamUser.appUser!.lastName}</div>
        )
    }
}


export default UpcomingQuizView;
