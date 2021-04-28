import React, { useContext, useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { BaseService } from "../../../services/BaseService";
import { AppContext } from "../../../context/AppContext";
import { IAppUserCustomDTO } from "../../../domain/custom/IAppUserCustomDTO";
import { IQuizDTO } from "../../../domain/IQuizDTO";
import { IQuizInvitationDTO } from "../../../domain/IQuizInvitationDTO";
import { FormatDate } from "../../../base/FormatDate";
import { ITeamUserDTO } from "../../../domain/ITeamUserDTO";


const MyQuizzes = () => {
    const history = useHistory();
    const appContext = useContext(AppContext);
    const [user, setUser] = useState({} as IAppUserCustomDTO);
    const [createdFinished, setCreatedFinished] = useState([] as IQuizDTO[]);
    const [createdUpcoming, setCreatedUpcoming] = useState([] as IQuizDTO[]);
    const [invPending, setInvPending] = useState([] as IQuizInvitationDTO[]);
    const [invAccepted, setInvAccepted] = useState([] as ITeamUserDTO[]);
    const [invFinished, setInvFinished] = useState([] as ITeamUserDTO[]);


    const data = async () => {
        await BaseService
            .getSingle<IAppUserCustomDTO>('quiz/userQuizzes', appContext.data.token)
            .then(data => {
                setUser(data!);
                console.log(data)
                if (data!.quizzes) {
                    setCreatedFinished(data!.quizzes.filter(a => a.finished));
                    setCreatedUpcoming(data!.quizzes.filter(a => !(a.finished)));
                }
                if (data!.quizInvitations) {
                    setInvPending(data!.quizInvitations.filter(a => a.pending));
                    setInvAccepted(data!.teamUsers.filter(a => !(a.team!.quiz!.finished)));
                    setInvFinished(data!.teamUsers.filter(a => a.team!.quiz!.finished));
                }

            });
    }

    const acceptInv = async (inv: IQuizInvitationDTO, accept: boolean) => {
        var entity = { id: inv.id, teamId: inv.teamId, appUserId: inv.appUserId, pending: false, accepted: accept } as IQuizInvitationDTO;
        await BaseService
            .updateEntity<IQuizInvitationDTO>(entity, "quizInvitations/accept", appContext.data.token)
            .then(d => {
                data();
            });

    }

    useEffect(() => {
        data();
    }, []);

    function viewQuiz(quiz: IQuizDTO, finished: boolean) {
        if (finished) {
            history.push("/finishedquiz/" + quiz.id)
        } else {
            history.push("/upcomingquiz/" + quiz.id)
        }
    }

    return (
        <div className="container border rounded">
            <p></p>
            <h3>Minu mälumängud</h3>
            <p></p>
            <div className="container border rounded">
                <p></p>
                Minu loodud mälumängud
                <p></p>
                <div className="container border rounded">
                    <p></p>
                    Tulevased mälumängud
                    <p></p>
                    <div className="container border rounded">
                        <table className="table table-hover" style={{ cursor: "pointer" }}>
                            <thead>
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Nimi</th>
                                    <th scope="col" className="text-right">Toimumisaeg</th>
                                </tr>
                            </thead>
                            <tbody>
                                {createdUpcoming.map((quiz, i) =>
                                (<tr onClick={e => viewQuiz(quiz, false)}>
                                    <th>{i + 1}</th>
                                    <th>{quiz.title}</th>
                                    <th className="text-right">{FormatDate(quiz.start)}</th>
                                </tr>)
                                )}
                            </tbody>
                        </table>
                    </div>
                </div>
                <p></p>
                <div className="container border rounded">
                    <p></p>
                    Lõppenud mälumängud
                    <p></p>
                    <div className="container border rounded">
                        <table className="table table-hover" style={{ cursor: "pointer" }}>
                            <thead>
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Nimi</th>
                                    <th scope="col" className="text-right">Toimus</th>
                                </tr>
                            </thead>
                            <tbody>
                                {createdFinished.map((quiz, i) =>
                                (<tr onClick={e => viewQuiz(quiz, true)}>
                                    <th>{i + 1}</th>
                                    <th>{quiz.title}</th>
                                    <th className="text-right">{FormatDate(quiz.start)}</th>
                                </tr>)
                                )}
                            </tbody>
                        </table>
                    </div>
                    <p></p>
                </div>
                <p></p>
            </div>
            <p></p>
            <div className="container border rounded">
                <p></p>
                Teiste mälumängud
                <p></p>
                <div className="container border rounded">
                    <p></p>
                    Ootel kutsed
                    <p></p>
                    <div className="container border rounded">
                        <table className="table table-hover" style={{ cursor: "pointer" }}>
                            <thead>
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Title</th>
                                    <th scope="col">Meeskond</th>
                                    <th scope="col" className="text-right">Start</th>
                                    <th scope="col"></th>
                                </tr>
                            </thead>
                            <tbody>
                                {invPending.map((inv, i) =>
                                (<tr>
                                    <th onClick={e => viewQuiz(inv.team!.quiz!, false)}>{i + 1}</th>
                                    <th onClick={e => viewQuiz(inv.team!.quiz!, false)}>{inv.team?.quiz?.title}</th>
                                    <th onClick={e => viewQuiz(inv.team!.quiz!, false)}>{inv.team?.name}</th>
                                    <th className="text-right" onClick={e => viewQuiz(inv.team!.quiz!, false)}>{FormatDate(inv.team!.quiz!.start)}</th>
                                    <th>
                                        <button style={{ zIndex: 99 }} type="button" className="btn btn-danger btn-sm float-right" onClick={e => acceptInv(inv, false)}>Keeldu</button>
                                        <button type="button" className="btn btn-success btn-sm float-right" onClick={e => acceptInv(inv, true)}>Liitu</button>
                                    </th>
                                </tr>)
                                )}
                            </tbody>
                        </table>
                    </div>
                </div>
                <p></p>
                <div className="container border rounded">
                    <p></p>
                    Tulevased mälumängud
                    <p></p>
                    <div className="container border rounded">
                        <table className="table table-hover" style={{ cursor: "pointer" }}>
                            <thead>
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Title</th>
                                    <th scope="col" className="text-right">Start</th>
                                </tr>
                            </thead>
                            <tbody>
                                {invAccepted.map((teamUser, i) =>
                                (<tr onClick={e => viewQuiz(teamUser.team!.quiz!, false)}>
                                    <th>{i + 1}</th>
                                    <th>{teamUser.team!.quiz!.title}</th>
                                    <th className="text-right">{FormatDate(teamUser.team!.quiz!.start)}</th>
                                </tr>)
                                )}
                            </tbody>
                        </table>
                    </div>
                </div>
                <p></p>
                <div className="container border rounded">
                    <p></p>
                    Lõppenud mälumängud
                    <p></p>
                    <div className="container border rounded">
                        <table className="table table-hover" style={{ cursor: "pointer" }}>
                            <thead>
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Title</th>
                                    <th scope="col" className="text-right">Start</th>
                                </tr>
                            </thead>
                            <tbody>
                                {invFinished.map((teamUser, i) =>
                                (<tr onClick={e => viewQuiz(teamUser.team!.quiz!, true)}>
                                    <th>{i + 1}</th>
                                    <th>{teamUser.team!.quiz!.title}</th>
                                    <th className="text-right">{FormatDate(teamUser.team!.quiz!.start)}</th>
                                </tr>)
                                )}
                            </tbody>
                        </table>
                    </div>
                </div>
                <p></p>
            </div>
            <p></p>
        </div>
    );
}

export default MyQuizzes;
