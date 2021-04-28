import React, { useContext, useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { BaseService } from "../../../services/BaseService";
import { AppContext } from "../../../context/AppContext";
import { IAppUserCustomDTO } from "../../../domain/custom/IAppUserCustomDTO";
import { IQuizDTO } from "../../../domain/IQuizDTO";
import { IQuizInvitationDTO } from "../../../domain/IQuizInvitationDTO";
import { FormatDate } from "../../../base/FormatDate";


const FriendQuizzes = () => {
    const history = useHistory();
    const appContext = useContext(AppContext);
    const [quizzes, setQuizzes] = useState([] as IQuizDTO[]);



    const data = async () => {
        await BaseService
            .getEntities<IQuizDTO>('quiz/friendQuizzes', appContext.data.token)
            .then(data => {
                console.log(data.sort((a, b) => (new Date(a.start).getTime() - new Date(b.start).getTime())))
                setQuizzes(data.sort((a, b) => (new Date(a.start).getTime() - new Date(b.start).getTime())))
                console.log(data)

            });
    }

    useEffect(() => {
        data();
    }, []);

    function viewQuiz(quiz: IQuizDTO) {
        if (quiz.finished) {
            history.push("/finishedquiz/" + quiz.id)
        }
        else{
            history.push("/upcomingquiz/" + quiz.id)
        }
    }

    return (
        <div className="container border rounded">
            <table className="table table-hover" style={{ cursor: "pointer" }}>
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Mängu nimi</th>
                        <th scope="col">Korraldaja</th>
                        <th scope="col" className="text-right">Toimumisaeg</th>
                    </tr>
                </thead>
                <tbody>
                    {quizzes.map((quiz, i) =>
                    (<tr onClick={e => viewQuiz(quiz)}>
                        <th>{i + 1}</th>
                        <th>{quiz.title}</th>
                        <th>{quiz.appUser?.firstName} {quiz.appUser?.lastName}</th>
                        <th className="text-right">{FormatDate(quiz.start)}</th>
                    </tr>)
                    )}
                </tbody>
            </table>
        </div>
    );
}

export default FriendQuizzes;
