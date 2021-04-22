import React, { useContext, useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { BaseService } from "../../../services/BaseService";
import { AppContext } from "../../../context/AppContext";
import { IAppUserCustomDTO } from "../../../domain/custom/IAppUserCustomDTO";
import { IQuizDTO } from "../../../domain/IQuizDTO";
import { IQuizInvitationDTO } from "../../../domain/IQuizInvitationDTO";


const MyQuizzes = () => {
    const history = useHistory();
    const appContext = useContext(AppContext);
    const [user, setUser] = useState({} as IAppUserCustomDTO);
    const [createdFinished, setCreatedFinished] = useState([] as IQuizDTO[]);
    const [createdUpcoming, setCreatedUpcoming] = useState([] as IQuizDTO[]);
    const [invPending, setInvPending] = useState([] as IQuizInvitationDTO[]);
    const [invAccepted, setInvAccepted] = useState([] as IQuizInvitationDTO[]);
    const [invFinished, setInvFinished] = useState([] as IQuizInvitationDTO[]);


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
                    setInvAccepted(data!.quizInvitations.filter(a => a.accepted && !(a.team!.quiz!.finished)));
                    setInvFinished(data!.quizInvitations.filter(a => a.accepted && a.team!.quiz!.finished));
                }

            });
    }

    useEffect(() => {
        data();
    }, []);

    function viewQuiz(quiz: IQuizDTO) {
        console.log(quiz)
        if (quiz.finished) {
            history.push("/finishedquiz/" + quiz.id)
        }
        else{
            history.push("/upcomingquiz/" + quiz.id)
        }
    }

    return (
        <div className="container border rounded">
            <p></p>
            <h3>MyQuizzes</h3>
            <p></p>
            <div className="container border rounded">
                <p></p>
                Created quizzes
                <p></p>
                <div className="container border rounded">
                    <p></p>
                    Upcoming
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
                                {createdUpcoming.map((quiz, i) =>
                                (<tr onClick={e => viewQuiz(quiz)}>
                                    <th>{i + 1}</th>
                                    <th>{quiz.title}</th>
                                    <th className="text-right">{quiz.start}</th>
                                </tr>)
                                )}
                            </tbody>
                        </table>
                    </div>
                </div>
                <p></p>
                <div className="container border rounded">
                    <p></p>
                    Finished
                    <p></p>
                    <div className="container border rounded">
                        <table className="table table-hover" style={{ cursor: "pointer" }}>
                            <thead>
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Title</th>
                                    <th scope="col" className="text-right">Finished</th>
                                </tr>
                            </thead>
                            <tbody>
                                {createdFinished.map((quiz, i) =>
                                (<tr onClick={e => viewQuiz(quiz)}>
                                    <th>{i + 1}</th>
                                    <th>{quiz.title}</th>
                                    <th className="text-right">{quiz.start}</th>
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
                Invited Quizzes
                <p></p>
                <div className="container border rounded">
                    <p></p>
                    Pending invitations
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
                                {invPending.map((inv, i) =>
                                (<tr onClick={e => viewQuiz(inv.team!.quiz!)}>
                                    <th>{i + 1}</th>
                                    <th>{inv.team?.quiz?.title}</th>
                                    <th className="text-right">{inv.team?.quiz?.start}</th>
                                </tr>)
                                )}
                            </tbody>
                        </table>
                    </div>
                </div>
                <p></p>
                <div className="container border rounded">
                    <p></p>
                    Accepted invitation
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
                                {invAccepted.map((inv, i) =>
                                (<tr onClick={e => viewQuiz(inv.team!.quiz!)}>
                                    <th>{i + 1}</th>
                                    <th>{inv.team?.quiz?.title}</th>
                                    <th className="text-right">{inv.team?.quiz?.start}</th>
                                </tr>)
                                )}
                            </tbody>
                        </table>
                    </div>
                </div>
                <p></p>
                <div className="container border rounded">
                    <p></p>
                    Finished
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
                                {invFinished.map((inv, i) =>
                                (<tr onClick={e => viewQuiz(inv.team!.quiz!)}>
                                    <th>{i + 1}</th>
                                    <th>{inv.team?.quiz?.title}</th>
                                    <th className="text-right">{inv.team?.quiz?.start}</th>
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
