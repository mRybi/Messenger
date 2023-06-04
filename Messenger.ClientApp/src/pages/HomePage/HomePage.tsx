import { FC } from "react";
import { AuthForm } from "./components";

export const HomePage: FC = () => {
    return (
        <div
            className="
          flex
          min-h-full 
          flex-col 
          justify-center 
          py-12 
          sm:px-6 
          lg:px-8 
          bg-gray-100
        "
        >
            <div className="sm:mx-auto sm:w-full sm:max-w-md">
                <img
                    height="48"
                    width="48"
                    className="mx-auto"
                    src="/logo.png"
                    alt="Logo"
                />
                <h2
                    className="
              mt-6 
              text-center 
              text-3xl 
              font-bold 
              tracking-tight 
              text-gray-900
            "
                >
                    Sign in to your account
                </h2>
            </div>
            <AuthForm />
        </div>
    );
}