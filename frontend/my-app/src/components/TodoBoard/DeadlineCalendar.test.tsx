import {DeadlineCalendar} from "./DeadlineCalendar.tsx";
import {render, screen } from "@testing-library/react";


const mockDeadline: string = "2025-05-26T11:23:17.000Z";


describe("DeadlineCalendar", () => {
    const renderComponent = (deadline: string | undefined) => {
        render(<DeadlineCalendar deadline={deadline} />);
    };

    it("renders placeholder if deadline is undefined", () => {
        renderComponent(undefined);
        expect(screen.getByText("-")).toBeInTheDocument();
    });

    it("renders date and time from deadline", () => {
        renderComponent(mockDeadline);
    });
    it( 'renders time', () => {
        renderComponent(mockDeadline);
        expect(screen.getByText("Monday, May 26")).toBeInTheDocument();
        expect(screen.getByText("14:23")).toBeInTheDocument();

    })

        it("renders disabled calendar", () => {
            renderComponent(mockDeadline);

            // StaticDatePicker генерує роль "grid" (таблиця з днями місяця)
            expect(screen.getByRole("grid")).toBeInTheDocument();
        });
});