export async function fetchWithTimeout(
    resource: RequestInfo,
    options: RequestInit,
    timeout: number
): Promise<Response>
{
    const controller = new AbortController();
    const id = setTimeout(() => controller.abort(), timeout);

    const response = await fetch(resource, {
        ...options,
        signal: controller.signal,
    });
    clearTimeout(id);

    return response;
}
