import {rest} from 'msw';
import type {Activity} from "@/generated/models";
import {setupServer} from "msw/node";

const handlers = [
    rest.get('activity', (req, res, context) => {
        return res(
            context.status(200),
            context.json<Activity>({
                id: 1,
                userId: 1,
                name: "just a test activity",
                minDuration: 2,
                maxDuration: 10,
                weekdayConstraint: "111100",
                recurringType: "DAILY",
                recurringInterval: 2,
                createTime: new Date()
            })
        )
    })
]

// start server with server.listen() and end it with server.close()
export const server = setupServer(...handlers);
